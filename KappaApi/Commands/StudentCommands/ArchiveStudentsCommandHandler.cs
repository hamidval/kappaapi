using AutoMapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.StudentCommands
{
    public class ArchiveStudentsCommandHandler : ICommandHandler<ArchiveStudentCommand>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IStudentQuery _studentQuery;
        private readonly ILessonQuery _lessonQuery;
        private readonly IParentQuery _parentQuery;
        private readonly IMapper _mapper;
        public ArchiveStudentsCommandHandler(ISessionFactory sessionFactory, IStudentQuery studentQuery, 
            IMapper mapper, IParentQuery parentQuery, ILessonQuery lessonQuery) 
        {
            _sessionFactory = sessionFactory;
            _studentQuery = studentQuery;
            _mapper = mapper;
            _parentQuery = parentQuery;
            _lessonQuery = lessonQuery;

        }
        public Task HandleAsync(ArchiveStudentCommand command)
        {
            var studentDto = _studentQuery.GetAllStudentsById(command.StudentId).First();
            var parent = _parentQuery.GetParentByStudentId(command.StudentId);
            var student = _mapper.Map<Student>(studentDto);

            student.Status = StudentStatus.Archived;
            student.Parent = parent;

            var lessonsDto = _lessonQuery.GetLessonByStudent(command.StudentId);
            var lessons = _mapper.Map<IList<Lesson>>(lessonsDto);

            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    session.Update(student);

                    foreach (Lesson lesson in lessons) 
                    {
                        lesson.Status = LessonStatus.Archived;
                    }

                    transaction.Commit();
                }
            }

            return Task.CompletedTask;
        }
    }
}
