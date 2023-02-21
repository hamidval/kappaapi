using KappaApi.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace KappaApi.NHibernateMappings
{
    public class InvoiceMap : ClassMapping<Invoice>
    {
        public InvoiceMap() 
        {
            Table("dbo.Invoice");

            Id(x => x.Id, m =>
                  m.Generator(Generators.Identity)
            );

            Property(x => x.CreatedOn);
            Property(x => x.StripeInvoiceId);
            Property(x => x.InvoiceStatus);
            Property(x => x.StripeInvoiceUrl);
            Property(x => x.InvoiceAmount);
            Property(x => x.ParentId);




        }
    }
}
