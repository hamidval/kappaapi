using AutoMapper;
using Hangfire.Annotations;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.ParentCommands
{
    public class ArchiveParentCommandHandler : ICommandHandler<ArchiveParentCommand>
    {
        private readonly IParentQuery _parentQuery;
        private readonly IStudentQuery _studentQuery;
        private readonly ILessonQuery _lessonQuery;
        private readonly IMapper _mapper;
        private readonly ISessionFactory _sessionFactory;
        public ArchiveParentCommandHandler(IParentQuery parentQuery, 
            IStudentQuery studentQuery,
            ILessonQuery lessonQuery,
            IMapper mapper,
            ISessionFactory sessionFactory
            ) 
        { 
            _parentQuery = parentQuery;
            _studentQuery = studentQuery;
            _lessonQuery = lessonQuery;
            _mapper = mapper;
            _sessionFactory = sessionFactory;
        }
        public Task HandleAsync(ArchiveParentCommand command)
        {
            var parentDto = _parentQuery.GetParentById(command.ParentId);
            var studentDto = _studentQuery.GetStudentsByParentId(command.ParentId);
            var lessonDtos = _lessonQuery.GetLessonByParent(command.ParentId);

            var parent = _mapper.Map<Parent>(parentDto);      
            var students = _mapper.Map<IList<Student>>(studentDto);
            var lessons = _mapper.Map<IList<Lesson>>(lessonDtos);

            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    parent.Status = ParentStatus.Archived;

                    foreach (Student student in students) 
                    {
                        student.Status = StudentStatus.Archived;
                    }

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
