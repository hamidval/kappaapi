using AutoMapper;
using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.TakenLessonCommands
{
    public class CreateTakenLessonCommandHandler : ICommandHandler<CreateTakenLessonCommand>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ILessonQuery _lessonQuery;
        private readonly ITakenLessonQuery _takenLessonQuery;
        private readonly IMapper _mapper;
        public CreateTakenLessonCommandHandler(ISessionFactory sessionFactory,
            ILessonQuery lessonQuery, IMapper mapper, ITakenLessonQuery takenLessonQuery) 
        {
            _sessionFactory = sessionFactory; 
            _lessonQuery = lessonQuery;
            _mapper = mapper;
            _takenLessonQuery = takenLessonQuery;
        }
        public Task HandleAsync(CreateTakenLessonCommand command)
        {
            var date = command.TakenLessonApiModel.Date;
            var teacherId = command.TakenLessonApiModel.TeacherId;
            List<TakenLesson> takenLessons = _mapper.Map<List<TakenLesson>>(command.TakenLessonApiModel.Lessons);

            foreach (var takenLesson in takenLessons) 
            {
                if (takenLesson.LessonPrice.LessonType == Enums.LessonType.OneToOne)
                {
                    takenLesson.TotalFee = takenLesson.Hours * takenLesson.LessonPrice.SingleFee;
                    takenLesson.TotalPay = takenLesson.Hours * takenLesson.LessonPrice.SinglePay;
                }
                else 
                {
                    takenLesson.TotalFee = takenLesson.Hours * takenLesson.LessonPrice.GroupFee;
                    takenLesson.TotalPay = takenLesson.Hours * takenLesson.LessonPrice.GroupFee;
                }

                takenLesson.LessonDate = command.TakenLessonApiModel.Date;
                takenLesson.Teacher.Id = command.TakenLessonApiModel.TeacherId;

            }

            var oldTakenLessonModels = _takenLessonQuery.GetTakenLessons(teacherId, date);
            List<TakenLesson> oldTakenLessons = _mapper.Map<List<TakenLesson>>(oldTakenLessonModels);


            using (NHibernate.ISession session = _sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    foreach (var item in oldTakenLessons)
                    {
                        session.Delete(item);
                    }

                    foreach (var item in takenLessons)
                    {
                        session.Save(item);
                    }
                    transaction.Commit();
                }

            }
            return Task.CompletedTask;
        }
    }
}
