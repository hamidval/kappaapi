using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KappaApi.Models.Api
{
    public class InvoiceSearchApiModel : IValidatableObject
    {
        [Required]
        [FromQuery(Name = "fromDate")]
        public DateTime FromDate { get; set; }

        [Required]
        [FromQuery(Name = "toDate")]
        public DateTime ToDate { get; set; }

       [FromQuery(Name = "invoiceId")]
        public int? InvoiceId { get; set; }
        
        [FromQuery(Name = "parentId")]
        public int? ParentId { get; set; }
       
        [FromQuery(Name = "stripeInvoiceId")]
        public string? StripeInvoiceId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromDate > ToDate) 
            {
                yield return new ValidationResult("From Date cannot be ahead of From Date", new[] { nameof(FromDate) });
            }

            if ((FromDate - ToDate).TotalDays > 100) 
            {
                yield return new ValidationResult("From Date cannot be ahead of From Date", new[] { nameof(FromDate) });
            }

            if (InvoiceId != null && InvoiceId <= 0) 
            {
                yield return new ValidationResult("Id must be greater than 0", new[] { nameof(InvoiceId) });
            }

        }
    }
}
