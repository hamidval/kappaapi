using KappaApi.Models;
using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface IInvoiceQuery
    {
        public List<InvoiceDto> GetInvoices(int? parentId, int? month, int? year);
        public List<InvoiceDto> GetInvoicesForInvoicePanel(DateTime fromDate, DateTime toDate, int? invoiceId,
            int? parentId, string? stripeInvoiceId, int pageNumber);


        public Invoice GetInvoiceByStripeId(string stripeInvoiceId);

        public List<string> GetInvoicesToSend();
    }
}
