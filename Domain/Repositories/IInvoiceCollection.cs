using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IInvoiceCollection
    {
        Task<long> InsertInvoiceAsync(Invoice invoice);
        Task<int> InsertInvoiceDetailAsync(InvoiceDetail invoiceDetail);
    }
}
