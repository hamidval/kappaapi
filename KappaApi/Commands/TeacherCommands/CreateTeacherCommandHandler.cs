using NHibernate;

namespace KappaApi.Commands.TeacherCommands
{
    public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand>
    {
        private readonly ISessionFactory _sessionFactory;
        public CreateTeacherCommandHandler(ISessionFactory sessionFactory) 
        {
            _sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateTeacherCommand command)
        {
            using (NHibernate.ISession session = _sessionFactory.OpenSession()) 
            {
                using (ITransaction transaction = session.BeginTransaction()) 
                {
                    session.Save(command.Teacher);
                    transaction.Commit();
                }

            }

            return Task.CompletedTask;
            
        }
    }
}
