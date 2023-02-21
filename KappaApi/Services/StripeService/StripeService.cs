using KappaApi.Models;
using KappaApi.Models.Dtos;
using Stripe;

namespace KappaApi.Services.StripeService
{
    public class StripeService : IStripeService
    {
   
        public Stripe.Invoice CreateInvoice(List<TakenLessonDto> takenLessonDtos, Parent parent)
        {
            //create invoice
            var icOptions = new InvoiceCreateOptions
            {
                Customer = parent.StripeCustomerId,
                CollectionMethod = "send_invoice",
                DueDate  = DateTime.UtcNow.AddDays(5)
                
            };
            var invoiceService = new InvoiceService();
            var invoice  = invoiceService.Create(icOptions);

            //create invoice items
            var invoiceItemService = new InvoiceItemService();
            foreach (var takenLesson in takenLessonDtos) 
            {
                var iiOptions = new InvoiceItemCreateOptions
                {
                    Invoice = invoice.Id,
                    Customer = parent.StripeCustomerId,
                    Amount = (long) takenLesson.TotalFee * 100,
                    Description = takenLesson.Subject.ToString() + takenLesson
                };

                invoiceItemService.Create(iiOptions);
            }

            var sentInvoice = invoiceService.SendInvoice(invoice.Id);

            return sentInvoice;
        }

        public void SendMoney()
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(Parent parent) 
        {
            var metaData = new Dictionary<string, string>();
            metaData.Add("id", parent.Id.ToString());

            var options = new CustomerCreateOptions
            {   Email = parent.Email,
                Name = parent.FirstName + " " +  parent.LastName,
                Metadata = metaData
            };
            var service = new CustomerService();
           var response =  service.Create(options);

            return response;
        }


    }
}
