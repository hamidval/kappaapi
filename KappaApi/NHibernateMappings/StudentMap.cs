using KappaApi.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KappaApi.NHibernateMappings
{
    public class StudentMap : ClassMapping<Student>
    {

        public StudentMap() 
        {
            Table("Student");

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

            Property(x => x.Status, m =>
            {
                m.Column("Status");
            });

            ManyToOne(x => x.Parent, m =>
            {
                m.Column("ParentId");

            });
        }

    }
}
