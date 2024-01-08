using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class UnitOfWorkRepositories : IUnitOfWorkRepositories
    {
        private readonly IProductCategoryCollection _categoryCollection;
        private readonly IProductCollection _productCollection;
        private readonly IInvoiceCollection _invoiceCollection;
        private readonly IUserCollection _userCollection;
        private readonly IRateCollection _rateCollection;

        public UnitOfWorkRepositories(
            IProductCategoryCollection categoryCollection,
            IProductCollection productCollection,
            IInvoiceCollection invoiceCollection,
            IUserCollection userCollection,
            IRateCollection rateCollection)
        {
            _categoryCollection = categoryCollection;
            _productCollection = productCollection;
            _invoiceCollection = invoiceCollection;
            _userCollection = userCollection;
            _rateCollection = rateCollection;
        }

        public IProductCategoryCollection ProductCategoryCollection { get => _categoryCollection; }

        public IProductCollection ProductCollection { get => _productCollection; }

        public IInvoiceCollection InvoiceCollection { get => _invoiceCollection; }

        public IUserCollection UserCollection { get => _userCollection; }

        public IRateCollection RateCollection { get => _rateCollection; }
       
    }
}
