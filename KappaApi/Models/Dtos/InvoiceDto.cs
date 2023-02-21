using KappaApi.Enums;

namespace KappaApi.Models.Dtos
{
    public class InvoiceDto
    {
        public InvoiceDto(int id, InvoiceStatus status, string stripeInvoiceUrl, 
            string parentName, decimal invoiceAmount, DateTime createdOn)
        {
            Id = id;
            Status = status;
            StripeInvoiceUrl = stripeInvoiceUrl;
            ParentName = parentName;
            InvoiceAmount = invoiceAmount;
            CreatedOn = createdOn;
        }

        public virtual int Id { get; set; }

        public virtual InvoiceStatus Status { get; set; }

        public virtual string StripeInvoiceUrl { get; set; }

        public virtual string ParentName { get; set; }

        public virtual decimal InvoiceAmount { get; set; }

        public virtual DateTime CreatedOn { get; set; }
    }
}
