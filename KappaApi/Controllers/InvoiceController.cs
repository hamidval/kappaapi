using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KappaApi.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceQuery _invoiceQuery;
        public InvoiceController(IInvoiceQuery invoiceQuery) 
        {
            _invoiceQuery = invoiceQuery;
        }

        [Route("/api/invoice/search")]
        [HttpGet]
        public List<InvoiceDto> GetInvoices(int? parentId, int? month, int? year) 
        {

            return _invoiceQuery.GetInvoices(parentId, month, year);
        }
       
    }
}
