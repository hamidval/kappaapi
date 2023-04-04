using KappaApi.Models;
using KappaApi.Models.Dtos;
using Stripe;

namespace KappaApi.Services.StripeService
{
    public interface IStripeService
    {
        public void SendMoney();
        public Stripe.Invoice CreateInvoice(List<TakenLessonDto> takenLessonDtos, Parent parent);
        public Customer CreateCustomer(Parent parent);
        public Refund CreateRefund(string stripeInvoiceId);
        public void SendInvoices(string id);
    }

   
}
