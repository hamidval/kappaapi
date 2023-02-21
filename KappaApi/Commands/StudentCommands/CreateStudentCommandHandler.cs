using NHibernate;

namespace KappaApi.Commands.StudentCommands
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand>
    {

        private readonly ISessionFactory _sessionFactory;
        public CreateStudentCommandHandler(ISessionFactory sessionFactory) 
        {
            _sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateStudentCommand command)
        {
            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    session.Save(command.Student);
                    transaction.Commit();
                }

            }
            return Task.CompletedTask;
        }
    }
}
