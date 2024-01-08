using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWorkAdapter _unitOfWorkAdapter;
        private readonly IRateService _rateService;

        public InvoiceService(IUnitOfWorkAdapter unitOfWorkAdapter, IRateService rateService)
        {
            _unitOfWorkAdapter = unitOfWorkAdapter;
            _rateService = rateService;
        }

        public async Task<ResultService> InsertInvoice(BillSaleDTO billSaleDTO)
        {
            IUnitOfWork context = null;
            try
            {
                context = _unitOfWorkAdapter.Create();
                
                var repositores = context.UnitOfWorkRepositories;                
                User user = await repositores.UserCollection.GetUserByIdentificationAsync(billSaleDTO.Invoice.Identification);
                if (user is null)
                {
                    user = new User()
                    {
                        Name = billSaleDTO.Invoice.NameClient,
                        LastName = billSaleDTO.Invoice.LastNameClient,
                        Identification = billSaleDTO.Invoice.Identification,
                        Email = billSaleDTO.Invoice.Email,
                        Phone = billSaleDTO.Invoice.Phone
                    };
                    
                    int result = await repositores.UserCollection.InsertUser(user);
                    user.Id = result;
                }

                Invoice invoice = new Invoice();
                invoice.NumInvoice = "";
                invoice.IdClient = user.Id;
                invoice.Identification = billSaleDTO.Invoice.Identification;
                invoice.NameClient = $"{user.Name} {user.LastName}";
                invoice.Date = DateTime.Now;                   
                invoice.State = true;
                invoice.Total = billSaleDTO.Invoice.Total;

                List<int> idProductCategories = billSaleDTO.InvoiceDetails
                    .Select(s => s.IdProductCategory.Value)
                    .ToList();

                var taxesForThisInvoice = await _rateService.GetTaxesForInvoice(idProductCategories);

                List<InvoiceDetail> invoiceDetailTaxes = new List<InvoiceDetail>();
                foreach (Rates tax in taxesForThisInvoice)
                {
                    decimal totalByProductCategory = 0;
                    decimal disccountTax = 0;

                    if (tax.IdProductCategory is null)
                    {
                        totalByProductCategory = billSaleDTO.Invoice.Total;                              
                        disccountTax = totalByProductCategory * tax.Rate;
                    }
                    else 
                    {
                        totalByProductCategory = billSaleDTO.InvoiceDetails
                            .Where(w => w.IdProductCategory == tax.IdProductCategory)
                            .Sum(s => s.Subtotal);

                        disccountTax = totalByProductCategory * tax.Rate;
                    }

                    invoiceDetailTaxes.Add(new InvoiceDetail()
                    {                           
                        IdRate = tax.Id,
                        Amount = 0,
                        State = 1,
                        IdProduct = null,
                        Imp = disccountTax,
                        Subtotal = disccountTax,
                        UnitPrice = 0
                    });

                }

                invoice.Total = billSaleDTO.Invoice.Total - invoiceDetailTaxes.Sum(s => s.Imp);
                long resultInvoice = await repositores.InvoiceCollection.InsertInvoiceAsync(invoice);

                foreach (var invoiceDetailTax in invoiceDetailTaxes)
                {
                    invoiceDetailTax.IdInvoice = (int)resultInvoice;           
                    await repositores.InvoiceCollection.InsertInvoiceDetailAsync(invoiceDetailTax);
                }                                      

                if (resultInvoice <= 0)
                {
                    context.Rollback();
                    return new ResultService
                    {
                        IsSuccess = false,
                        ErrorMessage = "Occurred an error inserting the invoice",
                    };
                }
                              
                foreach (InvoiceDetailDTO invoiceDetailDto in billSaleDTO.InvoiceDetails)
                {
                    int stockCurrentProduct = await repositores.ProductCollection.GetStockProductAsync(invoiceDetailDto.IdProduct);
                    if (stockCurrentProduct <= 0 || 
                        invoiceDetailDto.Amount >= stockCurrentProduct)
                    {
                        context.Rollback();
                        return new ResultService 
                        {
                            IsSuccess = false,
                            ErrorMessage = "There is no stock available about this product",                                
                        };
                    }
                                                                        
                    InvoiceDetail invoiceDetail = invoiceDetailDto.Adapt<InvoiceDetail>();
                    invoiceDetail.IdInvoice = (int)resultInvoice;
                    invoiceDetail.Imp = 0;
                    invoiceDetail.IdRate = null;
                    invoiceDetail.State = 1;
                    int resultInvoiceDetail = await repositores.InvoiceCollection.InsertInvoiceDetailAsync(invoiceDetail);
                    bool resultUpdateStock = await repositores.ProductCollection.UpdateStockProductAsync(invoiceDetailDto.IdProduct, invoiceDetailDto.Amount);                       
                }                   

                context.Save();
                context.Dispose();
                return new ResultService
                {
                    IsSuccess = true,
                    ErrorMessage = "Invoice succesfuly created",
                };                
            }
            catch (Exception ex)
            {
                //context.Rollback();
                context.Dispose();
                return new ResultService 
                {
                    IsSuccess = false,
                    ErrorMessage = "An error has occurred",
                };
            }
           
        }
    }
}

// valid if user exists
// -- true --> not save
// -- false --> save in the user table
// then loop the details of invoice
// -- valid if there is stock available
// ---- true --> we make the sale so save the invoice details with his invoice
// ------------- compute the taxes
// ---- false --> rollback the transaction
// -- commit the transaction