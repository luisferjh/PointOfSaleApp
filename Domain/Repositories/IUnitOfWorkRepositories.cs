using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWorkRepositories
    {      
        IProductCategoryCollection ProductCategoryCollection { get; }
        IProductCollection ProductCollection { get; }
        IInvoiceCollection InvoiceCollection { get; }
        IUserCollection UserCollection { get; }
        IRateCollection RateCollection { get; }
    }
}
