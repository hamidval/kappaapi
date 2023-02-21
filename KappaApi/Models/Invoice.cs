using KappaApi.Enums;
using Microsoft.Identity.Client;

namespace KappaApi.Models
{
    public class Invoice
    {
        public Invoice() 
        {
          
        }

        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }

        public virtual string StripeInvoiceId { get; set; }

        public virtual InvoiceStatus InvoiceStatus { get; set; }

        public virtual string StripeInvoiceUrl { get; set; }

        public virtual decimal InvoiceAmount { get; set; } 

        public virtual int ParentId { get; set; }
        
    }
}
