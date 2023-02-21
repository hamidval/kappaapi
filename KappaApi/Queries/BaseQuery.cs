namespace KappaApi.Queries
{
    public abstract class BaseQuery
    {
        public readonly string ConnectionString = DbConnectionFactory.ConnectionString;

    }
}
