using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Services.StripeService;
using NHibernate;
using Stripe;

namespace KappaApi.Commands.TakenLessonCommands
{
    public class RefundTakenLessonCommandHandler : ICommandHandler<RefundTakenLessonCommand>
    {
        private readonly ITakenLessonQuery _takenLessonQuery;
        private readonly IStripeService _stripeService;
        private readonly ISessionFactory _sessionFactory;

        public RefundTakenLessonCommandHandler(ITakenLessonQuery takenLessonQuery, 
            ISessionFactory sessionFactory, IStripeService stripeService)
        {
            _takenLessonQuery = takenLessonQuery;
            _sessionFactory = sessionFactory;
            _stripeService = stripeService;
        }

        public Task HandleAsync(RefundTakenLessonCommand command)
        {
            var takenLesson = _takenLessonQuery.GetTakenLessonById(command.TakenLessonId);

            if (takenLesson.StripeInvoiceId !=  null && takenLesson.StripeRefundId == null) 
            {
                var refund = _stripeService.CreateRefund(takenLesson.StripeInvoiceId);
                UpdateDb(refund, takenLesson);
            }           
            
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        public void UpdateDb(Refund refund, TakenLessonDto takenLesson) 
        {
            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    takenLesson.StripeRefundId = refund.Id;
                    session.Update(takenLesson);
                    transaction.Commit();
                }
            }
        }
    }
}
