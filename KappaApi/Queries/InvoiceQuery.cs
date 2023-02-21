using Dapper;
using KappaApi.Enums;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using System.Data.SqlClient;

namespace KappaApi.Queries
{
    public class InvoiceQuery :BaseQuery, IInvoiceQuery
    {
        public List<InvoiceDto> GetInvoices(int? parentId, int? month, int? year)
        {
            var sql = @"SELECT 
                            i.Id AS Id,
                            i.StripeInvoiceUrl AS Url,
                            i.InvoiceStatus AS Status,
                            i.InvoiceAmount AS InvoiceAmount,
                            (p.FirstName + ' ' +  p.LastName) as ParentName,
                            i.CreatedOn AS CreatedOn
                        FROM dbo.Invoice i
                            INNER JOIN dbo.Parent p ON i.ParentId = p.Id
                        WHERE 1=1 and i.ParentId is not null";
            if (month != null) 
            {
                sql += @" AND MONTH(i.CreatedOn) = @month";
            }

            if (year != null)
            {
                sql += @" AND YEAR(i.CreatedOn) = @year";
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new { month = month, year = year })
                    .Select(x => {

                        var invoice = new InvoiceDto
                        (
                            x.Id,
                            (InvoiceStatus) x.Status,
                            x.Url,
                            x.ParentName,
                            (decimal) x.InvoiceAmount,
                            (DateTime) x.CreatedOn
                        );
                        
                        return invoice;

                    }
                    ).ToList();
            }
        }
    }
}
