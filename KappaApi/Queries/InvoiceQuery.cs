using Dapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace KappaApi.Queries
{
    public class InvoiceQuery :BaseQuery, IInvoiceQuery
    {


        public List<string> GetInvoicesToSend() 
        {
            var sql = @"SELECT *
                            i.StripeInvoiceId AS StripeInvoiceId
                        FROM dbo.Invoice i
                        WHERE i.InvoiceStatus = 1"; //is Open;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<List<string>>(sql);
            }
        }

        public Invoice GetInvoiceByStripeId(string stripeInvoiceId)
        {
            var sql = @"SELECT TOP 1
                            i.Id AS Id,
                            i.StripeInvoiceUrl AS StripeInvoiceUrl,
                            i.InvoiceStatus AS InvoiceStatus,
                            i.InvoiceAmount AS InvoiceAmount,
                            i.ParentId AS ParentId,
                            i.CreatedOn AS CreatedOn,
                            i.StripeInvoiceId AS StripeInvoiceId
                        FROM dbo.Invoice i
                        WHERE i.StripeInvoiceId = @stripeInvoiceId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<Invoice>(sql, new { stripeInvoiceId = stripeInvoiceId }); 
            }
        }

        public List<InvoiceDto> GetInvoicesForInvoicePanel(DateTime fromDate, DateTime toDate, int? invoiceId, 
            int? parentId, string? stripeInvoiceId, int pageNumber)
        {
            var offset = (pageNumber - 1) * 10;

            var sql = @"SELECT 
                            i.Id AS Id,
                            i.StripeInvoiceUrl AS Url,
                            i.InvoiceStatus AS Status,
                            i.InvoiceAmount AS InvoiceAmount,
                            (p.FirstName + ' ' +  p.LastName) as ParentName,
                            i.CreatedOn AS CreatedOn,
                            i.StripeInvoiceId
                        FROM dbo.Invoice i
                            INNER JOIN dbo.Parent p ON i.ParentId = p.Id
                        WHERE 
                                i.createdOn >= @fromDate 
                            AND i.createdOn <= @toDate ";

            if (invoiceId != null && invoiceId > 0)
            {
                sql += @" AND i.Id = @invoiceId";
            }

            if (parentId != null && parentId > 0)
            {
                sql += @" AND i.ParentId = @parentId";
            }

            if (!string.IsNullOrWhiteSpace(stripeInvoiceId))
            {
                sql += @" AND i.StripeInvoiceId = @stripeInvoiceId";
            }

            sql += " ORDER BY i.Id OFFSET @offset ROWS FETCH NEXT 10 ROWS ONLY;";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new {
                         fromDate,
                         toDate,
                         invoiceId,
                         parentId,
                         stripeInvoiceId,
                         offset})
                    .Select(x => {

                        var invoice = new InvoiceDto
                        (
                            x.Id,
                            (InvoiceStatus)x.Status,
                            x.Url,
                            x.ParentName,
                            (decimal)x.InvoiceAmount,
                            (DateTime)x.CreatedOn,
                            x.StripeInvoiceId
                        );

                        return invoice;

                    }
                    ).ToList();
            }
        }

        public List<InvoiceDto> GetInvoices(int? parentId, int? month, int? year)
        {
            var sql = @"SELECT 
                            i.Id AS Id,
                            i.StripeInvoiceUrl AS Url,
                            i.InvoiceStatus AS Status,
                            i.InvoiceAmount AS InvoiceAmount,
                            (p.FirstName + ' ' +  p.LastName) as ParentName,
                            i.CreatedOn AS CreatedOn,
                            i.StripeInvoiceId
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
                            (DateTime) x.CreatedOn,
                            x.StripeInvoiceId
                        );
                        
                        return invoice;

                    }
                    ).ToList();
            }
        }
    }
}
