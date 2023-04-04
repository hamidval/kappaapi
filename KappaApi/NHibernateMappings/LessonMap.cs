using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using KappaApi.Models;
namespace KappaApi.NHibernateMappings
{
    public class LessonMap : ClassMapping<Lesson>
    {

        public LessonMap() 
        {
            Table("Lesson");

            Id(x => x.Id, m =>
                m.Generator(Generators.Identity)
            );

            Property(x => x.StartDate, m =>
                m.Column("StartDate")
            );

            Property(x => x.Day, m =>
                m.Column("Day")
            );

            Property(x => x.Status, m =>
                m.Column("Status")
            );

            Property(x => x.EndDate, m =>
                m.Column("EndDate")
            );

            ManyToOne(x => x.Student, m =>
                m.Column("StudentId")
            );

            ManyToOne(x => x.Teacher, m =>
                m.Column("TeacherId")
            );

            Component(x => x.LessonPrice, c => 
            {
                c.Property(x => x.YearGroup);
                c.Property(x => x.Subject);
                c.Property(x => x.GroupPay);
                c.Property(x => x.GroupFee);
                c.Property(x => x.SinglePay);
                c.Property(x => x.SingleFee);
                c.Property(x => x.LessonType);

            });

        }
    }
}
