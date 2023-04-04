namespace KappaApi.Models.Api
{
    public class QueryLessonApiModel
    {
        public string? InvoiceId { get; set; }
        public string? StripeInvoiceId { get; set; }
        public int? ParentId { get; set; }
    }
}
