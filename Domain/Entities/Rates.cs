using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rates
    {
        public int Id { get; set; }
        public int? IdProductCategory { get; set; }
        public string Tax { get; set; }        
        public decimal Rate { get; set; }
    }
}
