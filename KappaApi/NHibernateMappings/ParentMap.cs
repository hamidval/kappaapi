using KappaApi.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KappaApi.NHibernateMappings
{
    public class ParentMap : ClassMapping<Parent>
    {
        public ParentMap() 
        {
            Table("Parent");

            Id(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property(x => x.FirstName, m =>
            {
                m.Column("FirstName");
            });

            Property(x => x.LastName, m =>
            {
                m.Column("LastName");
            });

            Property(x => x.Email, m =>
            {
                m.Column("Email");
            });

            Property(x => x.StripeCustomerId, m =>
            {
                m.Column("StripeCustomerId");
            });

        }

    }
}
