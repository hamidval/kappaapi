using KappaApi.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KappaApi.NHibernateMappings
{
    public class TakenLessonMap : ClassMapping<TakenLesson>
    {
        public TakenLessonMap() 
        {
            Table("TakenLesson");

            Id(x => x.Id, m =>
                m.Generator(Generators.Identity));

            Property(x => x.Hours);
            Property(x => x.TotalPay);
            Property(x => x.TotalFee);
            Property(x => x.LessonDate);
            Property(x => x.TotalFee);
            Property(x => x.InvoiceId);
            Property(x => x.StripeInvoiceId);
            Property(x => x.StripeRefundId);
            Property(x => x.TakenLessonPaidStatus);
            

            ManyToOne(x => x.Teacher, m =>
                m.Column("TeacherId")
            );

            ManyToOne(x => x.Student, m =>
                m.Column("StudentId")
            );

            Component(x => x.LessonPrice, c =>
            {
                c.Property(x => x.YearGroup);
                c.Property(x => x.Subject);
                c.Property(x => x.SinglePay);
                c.Property(x => x.SingleFee);
                c.Property(x => x.GroupPay);
                c.Property(x => x.GroupFee);
                c.Property(x => x.LessonType);
            });     

        }
    }
}
