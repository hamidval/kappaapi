using AutoMapper;
using KappaApi.Domain;
using KappaApi.Models;
using KappaApi.Services.StripeService;
using NHibernate;

namespace KappaApi.Commands.LessonCommands
{
    public class CreateParentStudentLessonCommandHandler : ICommandHandler<CreateParentStudentLessonCommand>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;
        private readonly IStripeService _stripeService;
        public CreateParentStudentLessonCommandHandler(ISessionFactory sessionFactory, IMapper mapper,
            IStripeService stripeService) 
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
            _stripeService = stripeService;

        }
        public Task HandleAsync(CreateParentStudentLessonCommand command)
        {
            var model = command.ParentStudentLessonApiModel;
            var parent = _mapper.Map<Parent>(model);
            var customer = _stripeService.CreateCustomer(parent);
            using (NHibernate.ISession session  = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    parent.StripeCustomerId = customer.Id;
                    session.Save(parent);
                    
                    if (model.Students != null) 
                    {
                        foreach (var student in model.Students)
                        {
                            var _student = _mapper.Map<Student>(student);
                            _student.Parent = parent;
                            
                            session.Save(_student);
                            foreach (var lesson in student.Lessons) 
                            {
                                var newLesson = new Lesson();

                                var price = new LessonPrice(lesson.Subject, lesson.YearGroup,
                                    lesson.LessonType, lesson.SingleFee, lesson.GroupFee,
                                    lesson.SingleFee, lesson.SinglePay);
                                newLesson.Teacher.Id = lesson.TeacherId;
                                newLesson.LessonPrice = price;
                                newLesson.Student = _student;
                                newLesson.StartDate = lesson.StartDate;
                                newLesson.EndDate = lesson.EndDate;
                                session.Save(newLesson);
                            }
                        }


                    }   
                    transaction.Commit();
                }
            }
            return Task.CompletedTask;
        }
    }
}
