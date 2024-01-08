using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductCollection
    {
        Task InsertProductAsync(Product product);           
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<int> GetStockProductAsync(int idProduct);
        Task<bool> UpdateStockProductAsync(int idProduct, int stockSubstract);
    }
}
