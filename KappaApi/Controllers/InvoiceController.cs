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

        [HttpGet("search")]
        public InvoicePanelDto GetInvoices([FromQuery] InvoiceSearchApiModel model)
        {

            var viewModel = new InvoicePanelDto();

            viewModel.Invoices = _invoiceQuery.GetInvoicesForInvoicePanel(
                model.FromDate,
                model.ToDate,
                model.InvoiceId,
                model.ParentId,
                model.StripeInvoiceId,
                model.PageNumber
                );

            var prev = model.PageNumber - 1;
            var next = model.PageNumber + 1;
            if (prev <= 0)
            {
                viewModel.HasPrev = false;
            }
            else
            {
                viewModel.HasPrev =
                    _invoiceQuery.GetInvoicesForInvoicePanel(model.FromDate, model.ToDate,
                                                            model.InvoiceId, model.ParentId,
                                                            model.StripeInvoiceId, prev).Any();
            }


            viewModel.HasNext = _invoiceQuery.GetInvoicesForInvoicePanel(model.FromDate, model.ToDate,
                                                        model.InvoiceId, model.ParentId,
                                                        model.StripeInvoiceId, next).Any();


            return viewModel;

        }
    }
}
