using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface IInvoiceQuery
    {
        public List<InvoiceDto> GetInvoices(int? parentId, int? month, int? year);
    }
}
