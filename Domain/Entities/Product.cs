using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string IdCategory { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }    
        public decimal UnitPrice { get; set; }
        public int State { get; set; }
    }
}
