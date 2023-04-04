using KappaApi.Models;
using KappaApi.Queries.Contracts;
using NHibernate;

namespace KappaApi.Commands.StudentCommands
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand>
    {

        private readonly ISessionFactory _sessionFactory;
        private readonly IParentQuery _parentQuery;
        public CreateStudentCommandHandler(ISessionFactory sessionFactory, IParentQuery parentQuery) 
        {
            _sessionFactory = sessionFactory;
            _parentQuery = parentQuery;
        }

        public Task HandleAsync(CreateStudentCommand command)
        {
            var parentId = command.Student.ParentId;
            var parent = _parentQuery.GetParentById(parentId);
            if (parent != null) 
            {
                using (NHibernate.ISession session = _sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var student = new Student(parent);
                        student.FirstName = command.Student.FirstName;
                        student.LastName = command.Student.LastName;
                        student.Status = Enums.StudentStatus.Active;
                        student.Parent = parent;

                        session.Save(student);
                        transaction.Commit();
                    }

                }

            }
          
            return Task.CompletedTask;
        }
    }
}
