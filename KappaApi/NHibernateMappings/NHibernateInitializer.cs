using KappaApi.Models;
using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Caches.SysCache;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System.Reflection;

namespace KappaApi.NHibernateMappings
{
    public static class NHibernateInitializer
    {
        public static ISessionFactory GetSessionFactory(string configurationName = "command", string connectionstring = null)
        {
            var commandConfiguration = BuildDatabaseConfiguration(configurationName, connectionstring);
            return commandConfiguration.BuildSessionFactory();
        }
        public static Configuration BuildDatabaseConfiguration(string name, string connectionString)
        {
            var configuration = new Configuration();

            configuration.Proxy(p => p.ProxyFactoryFactory<StaticProxyFactoryFactory>())
                         .DataBaseIntegration(db =>
                         {
                             db.ConnectionString = connectionString;
                             db.Dialect<MsSql2008Dialect>();
                             db.Driver<Sql2008ClientDriver>();
                         })
                         .AddAssembly(typeof(Parent).Assembly);

            configuration.SessionFactory()
                .Named(name)
                .Caching.Through<SysCacheProvider>()
                .WithDefaultExpiration(1440);

            var compiledMapping = GetModelMapper().CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddMapping(compiledMapping);

            return configuration;
        }

        public static ModelMapper GetModelMapper()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(ParentMap).Assembly.GetExportedTypes());
            mapper.BeforeMapProperty += MapperOnBeforeMapProperty;

            return mapper;
        }

        private static void MapperOnBeforeMapProperty(IModelInspector modelinspector, PropertyPath member, IPropertyMapper propertycustomizer)
        {
            var info = (PropertyInfo)member.LocalMember;

            if (info.PropertyType == typeof(string))
            {
                propertycustomizer.Type(NHibernateUtil.AnsiString);
            }
        }
    }
}
