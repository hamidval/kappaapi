using Microsoft.Identity.Client;

namespace KappaApi.Queries.Dtos.TakenLessonQuery
{
    public class TakenLessonPanelDto
    {
        public int Id { get; set; }
        public string Teacher { get; set; }

        public string Student { get; set; }

        public decimal TotalPay { get; set; }

        public string TotalFee { get; set; }

        public DateTime LessonDate { get; set; }

        public int InvoiceId { get; set; }

        public string StripeInvoiceId { get; set; }

        public string StripeRefundId { get; set; }

        public string Status { get; set; }

        public decimal Hours { get; set; }

        public string Subject { get; set; }
    }
}
