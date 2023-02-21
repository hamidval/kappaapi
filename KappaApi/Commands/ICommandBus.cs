namespace KappaApi.Commands
{
    public interface ICommandBus
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class;
    }
}
