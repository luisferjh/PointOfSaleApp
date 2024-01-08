using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        public int Id { get; set; }        
        public int IdClient { get; set; }
        public string NameClient { get; set; }  
        public string Identification { get; set; }   
        public string NumInvoice { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public bool State { get; set; }

    }
}
