using KappaApi.Models;
using KappaApi.NHibernateMappings;
using KappaApi.Services.StripeService;
using NHibernate;

namespace KappaApi.Commands.ParentCommands
{
    public class CreateParentCommandHandler : ICommandHandler<CreateParentCommand>
    {
        private readonly ISessionFactory _session;
        private readonly IStripeService _stripeService;
        public CreateParentCommandHandler(ISessionFactory session, IStripeService stripeService) 
        {
            _session = session;
            _stripeService = stripeService;
        }
        public Task HandleAsync(CreateParentCommand command)
        {
            var parent = command.Parent; 
            
            var customer = _stripeService.CreateCustomer(parent);

            parent.StripeCustomerId = customer.Id;

            using (NHibernate.ISession session = _session.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(parent);
                    transaction.Commit();
                }
            }

            return Task.CompletedTask;
        }
    }
}
