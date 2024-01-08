using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRateService
    {
        Task<List<Rates>> GetTaxesForInvoice(List<int> idProductCategories);
    }
}
