using KappaApi.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KappaApi.NHibernateMappings
{
    public class TeacherMap : ClassMapping<Teacher>
    {
        public TeacherMap() 
        {
            Table("Teacher");

            Id(x => x.Id, m =>
                m.Generator(Generators.Identity)
            ); ;

            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.Email);
        }

        
    }
}
