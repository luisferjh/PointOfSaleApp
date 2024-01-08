using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RateService : IRateService
    {
        private readonly IUnitOfWorkAdapter _unitOfWorkAdapter;
        private readonly IRateCollection _rateCollection;

        public RateService(IUnitOfWorkAdapter unitOfWorkAdapter, IRateCollection rateCollection)
        {
            _unitOfWorkAdapter = unitOfWorkAdapter;
            _rateCollection = rateCollection;
        }

        public async Task<List<Rates>> GetTaxesForInvoice(List<int> idProductCategories)
        {
            List<Rates> taxes = new List<Rates>();
            IUnitOfWork context = null;
            try
            {
                context = _unitOfWorkAdapter.Create();
                
                var taxesForAllProduct = await context.UnitOfWorkRepositories.RateCollection.GetRatesForAllProductCategories();
                taxes.AddRange(taxesForAllProduct);
                foreach (int item in idProductCategories)
                {
                    var taxesForItemProduct = await context.UnitOfWorkRepositories.RateCollection.GetRatesByProductCategory(item);
                    taxes.AddRange(taxesForItemProduct);
                }                                                                                     
                
                return taxes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
