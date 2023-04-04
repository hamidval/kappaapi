using AutoMapper;
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
        private readonly IMapper _mapper;
        public CreateParentCommandHandler(ISessionFactory session, IStripeService stripeService, IMapper mapper) 
        {
            _session = session;
            _stripeService = stripeService;
            _mapper = mapper;
        }
        public Task HandleAsync(CreateParentCommand command)
        {
            var parentModel = command.Parent;
            var parent = _mapper.Map<Parent>(parentModel); 
            
            var customer = _stripeService.CreateCustomer(parent);

            parent.StripeCustomerId = customer.Id;
            parent.Status = Enums.ParentStatus.Active;

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
