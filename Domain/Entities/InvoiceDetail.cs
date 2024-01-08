using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InvoiceDetail
    {
        public int Id { get; set; }  
        public int IdInvoice { get; set; }  
        public int? IdProduct { get; set; }
        public int? IdRate { get; set; }
        public int Amount { get; set; }  
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Imp { get; set; }
        public int State { get; set; }
    }
}
