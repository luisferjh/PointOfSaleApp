using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductCategoryCollection
    {
        Task<List<ProductCategory>> GetAllProductCategoryAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
    }
}
