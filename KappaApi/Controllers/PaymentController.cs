using KappaApi.Commands;
using KappaApi.Commands.TakenLessonCommands;
using KappaApi.Services.StripeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace KappaApi.Controllers
{
    [Route("/api/payment")]
    [ApiController]    
    public class PaymentController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public PaymentController(ICommandBus commandBus) 
        {
            _commandBus = commandBus;
        }
                
        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            const string Key = "";
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();          

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                 Request.Headers["Stripe-Signature"], Key);
                // var stripeEvent = EventUtility.ParseEvent(json);
                // Handle the event
                if (stripeEvent.Type == (Events.InvoicePaid))
                {
                    var invoice = stripeEvent.Data.Object as Invoice;

                    if (invoice != null)
                    {
                        var stripeInvoiceId = invoice?.Id;
                        var status = invoice?.Status;

                        var command = new UpdatePaidTakenLessonCommand();
                        command.Invoice = invoice;

                        _commandBus.SendAsync(command);
                    }
                    else 
                    {
                        Console.WriteLine("null invoice");
                    }
                    


                    Console.WriteLine("Invoice payment made");
                }                
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }
    }

}
