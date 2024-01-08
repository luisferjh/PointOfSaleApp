using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceDetailDTO
    {
        public int? IdProductCategory { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }       
    }
}
