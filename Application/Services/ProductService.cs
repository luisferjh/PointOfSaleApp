using Application.Interfaces;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductCollection _productCollection;

        public ProductService(IProductCollection productCollection)
        {
            _productCollection = productCollection;
        }

        public Task<int> GetStockProductAsync(int IdProduct)
        {
            throw new NotImplementedException();
        }
    }
}
