using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallerInvoiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<ActionResult> PostInvoice([FromBody] BillSaleDTO billSaleDTO) 
        {
            var resultService = await _invoiceService.InsertInvoice(billSaleDTO);
            if (resultService.IsSuccess)
                return Ok(resultService);
            else 
                return BadRequest(resultService);
        }
           
    }
}
