using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRateCollection
    {       
        Task<Rates> GetRateByIdAsync(int id);
        Task<List<Rates>> GetRatesByProductCategory(int idProductCategory);
        Task<List<Rates>> GetRatesForAllProductCategories();
    }
}
