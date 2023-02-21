using AutoMapper;
using KappaApi.Domain;
using KappaApi.Models;
using NHibernate;

namespace KappaApi.Commands.LessonCommands
{
    public class CreateLessonCommandHandler : ICommandHandler<CreateLessonCommand>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;
        public CreateLessonCommandHandler(ISessionFactory sessionFactory, IMapper mapper) 
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
        }
        public Task HandleAsync(CreateLessonCommand command)
        {   
            var model = command.Lesson;
            var lesson = _mapper.Map<Lesson>(model);
            var price = new LessonPrice(model.Subject, model.YearGroup, model.LessonType,
                model.SingleFee, model.GroupFee, model.SinglePay, model.SingleFee);
            lesson.LessonPrice = price;
            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {                    
                    session.Save(lesson);
                    transaction.Commit();
                }

                return Task.CompletedTask;
            }
        }
    }
}
