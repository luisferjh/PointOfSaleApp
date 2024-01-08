using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceDTO
    {       
        public string NameClient { get; set; }
        public string LastNameClient { get; set; }
        public string Identification { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Total { get; set; }
    }
}
