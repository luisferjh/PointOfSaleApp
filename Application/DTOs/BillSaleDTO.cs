using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BillSaleDTO
    {
        public InvoiceDTO Invoice { get; set; }
        public List<InvoiceDetailDTO> InvoiceDetails { get; set; }
    }
}
