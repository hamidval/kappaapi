using AutoMapper;
using KappaApi.Domain;
using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.LessonCommands
{
    public class EditLessonCommandHandler : ICommandHandler<EditLessonCommand>
    {
       
        private readonly ISessionFactory _sessionFactory;
        private readonly ITeacherQuery _teacherQuery;

        public EditLessonCommandHandler(ISessionFactory sessionFactory,
            ITeacherQuery teacherQuery, IStudentQuery studentQuery) 
        {
            _teacherQuery = teacherQuery;
            _sessionFactory = sessionFactory;
        }
        public Task HandleAsync(EditLessonCommand command)
        {

            if (command.Lesson.Id > 0)
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction()) 
                    {
                        var teacher = _teacherQuery.GetTeachers(command.Lesson.TeacherId).First();

                        var lesson = session.Get<Lesson>(command.Lesson.Id);
                        lesson.Teacher = Lesson.CreateTeacher(teacher.Id, teacher.FirstName, teacher.LastName, teacher.Email);
                        lesson.StartDate = command.Lesson.StartDate;
                        lesson.EndDate = command.Lesson.EndDate;
                        lesson.LessonPrice = Lesson.CreateLessonPrice(
                                                            command.Lesson.Subject,
                                                            command.Lesson.YearGroup,
                                                            Enums.LessonType.Group,
                                                            command.Lesson.SingleFee,
                                                            command.Lesson.GroupFee,
                                                            command.Lesson.SinglePay,
                                                            command.Lesson.GroupPay);
                        lesson.Status = Enums.LessonStatus.Active;
                        lesson.Day = 0;

                        session.SaveOrUpdate(lesson);
                        transaction.Commit();

                    }
                    

                };

            }
            return Task.CompletedTask;
        }
    }
}
