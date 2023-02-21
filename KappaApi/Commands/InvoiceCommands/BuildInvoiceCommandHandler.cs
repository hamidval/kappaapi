using AutoMapper;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Services.StripeService;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NHibernate;

namespace KappaApi.Commands.InvoiceCommands
{
    public class BuildInvoiceCommandHandler : ICommandHandler<BuildInvoiceCommand>
    {
        private readonly ITakenLessonQuery _takenLessonQuery;
        private readonly IStripeService _stripeService;
        private readonly ISessionFactory _sessionFactory;
        private readonly IParentQuery _parentQuery;
        private readonly IMapper _mapper;
        public BuildInvoiceCommandHandler(ITakenLessonQuery takenLessonQuery, 
            IStripeService stripeService, IParentQuery parentQuery, IMapper mapper, ISessionFactory sessionFactory) 
        {
            _takenLessonQuery = takenLessonQuery;
            _stripeService = stripeService;
            _parentQuery = parentQuery;
            _mapper = mapper;
            _sessionFactory = sessionFactory;
        }
        public Task HandleAsync(BuildInvoiceCommand command)
        {       
            using (NHibernate.ISession session = _sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    foreach (var parentId in command.ParentIds)
                    {
                        var takenLessonDtos = _takenLessonQuery.GetUninvoicedTakenLessons(parentId).ToList();
                        var parent = _parentQuery.GetParentById(parentId);

                        var invoice = new Invoice();

                        Stripe.Invoice stripeInvoice = _stripeService.CreateInvoice(takenLessonDtos, parent);

                        var takenLessons = _mapper.Map<List<TakenLesson>>(takenLessonDtos);


                        foreach (var takenLesson in takenLessons)
                        {
                            takenLesson.StripeInvoiceId = stripeInvoice.Id;
                        }

                        invoice.StripeInvoiceId = stripeInvoice.Id;
                        invoice.StripeInvoiceUrl = stripeInvoice.HostedInvoiceUrl;
                        invoice.InvoiceAmount = stripeInvoice.AmountDue;
                        invoice.ParentId = parentId;

                        foreach (var takenLesson in takenLessons)
                        {
                            session.Update(takenLesson);
                        }

                        invoice.CreatedOn = DateTime.Now;

                        session.Save(invoice);
                        
                    }
                    transaction.Commit();
                }

            }               
            
            return Task.CompletedTask;
        }
    }
}
