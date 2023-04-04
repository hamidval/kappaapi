using AutoMapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.TakenLessonCommands
{
    public class UpdatePaidTakenLessonCommandHandler : ICommandHandler<UpdatePaidTakenLessonCommand>
    {
        private readonly ITakenLessonQuery _takenLessonQuery;
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;
        private readonly IInvoiceQuery _invoiceQuery;
        public UpdatePaidTakenLessonCommandHandler(ITakenLessonQuery takenLessonQuery, ISessionFactory sessionFactory, IMapper mapper, IInvoiceQuery invoiceQuery) 
        {
            _takenLessonQuery = takenLessonQuery;
            _sessionFactory = sessionFactory;
            _mapper = mapper;
            _invoiceQuery = invoiceQuery;

        }

        public Task HandleAsync(UpdatePaidTakenLessonCommand command)
        {
            var invoice = command.Invoice;
            int status = 0;
            StripeConstants.StripeInvoiceStatuses.TryGetValue(invoice.Status,out status);

            var takenLessonDtos = _takenLessonQuery.GetTakenLessonsByStripeInvoiceId(invoice.Id);
            var takenLessons = _mapper.Map<List<TakenLesson>>(takenLessonDtos);
            var invoiceModel = _invoiceQuery.GetInvoiceByStripeId(invoice.Id);            

            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    invoiceModel.InvoiceStatus = (InvoiceStatus) status;
                    session.Update(invoiceModel);

                    foreach (var takenLesson in takenLessons) 
                    {
                        takenLesson.TakenLessonPaidStatus = (TakenLessonPaidStatus) status;
                        session.Update(takenLesson);
                    }

                    transaction.Commit();
                }
            }
                return Task.CompletedTask;
        }
    }
}
