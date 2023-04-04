using Dapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.TakenLessonQuery;
using Microsoft.Data.SqlClient;
using Stripe;
using System.Security.Cryptography;

namespace KappaApi.Queries
{
    public class TakenLessonQuery : BaseQuery, ITakenLessonQuery
    {
       
        public TakenLessonQuery() 
        {

        }
        public TakenLessonDto GetTakenLessonById(int id)
        {
            var sql = @"
                        SELECT TOP 1
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate,
                            tl.StripeInvoiceId AS StripeInvoiceId
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
                        WHERE tl.Id = @id
                        ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<TakenLessonDto>(sql, new { id = id });                    
            }
        }
        public IList<TakenLessonModelDto> GetTakenLessonsByStripeInvoiceId(string id)
        {
            var sql = @"
                        SELECT 
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate,
                            tl.StripeInvoiceId AS StripeInvoiceId,
                            tl.StripeRefundId AS StripeRefundId,
                            tl.InvoiceId AS InvoiceId,
                            tl.TakenLessonPaidStatus AS TakenLessonPaidStatus
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                        INNER JOIN dbo.Parent p ON p.Id = s.ParentId
                        WHERE tl.StripeInvoiceId = @id
                        ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<TakenLessonModelDto>(sql, new { id = id }).ToList();
            }
        }

        public IList<RegisterTakenLessonDto> GetTakenLessons(int teacherId, DateTime date)
        {
            var sql = @"
                        SELECT                           
                            tl.Id AS Id,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,                            
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentName,                            
                            s.Id AS StudentId,                            
                            tl.Hours AS Hours,
                            tl.TakenLessonPaidStatus AS Status,
                            slut.Subject AS Subject,
                            tl.StripeInvoiceId AS StripeInvoiceId,
                            tl.StripeRefundId AS StripeRefundId,
                            tl.InvoiceId AS InvoiceId,
							(CASE 
								WHEN EXISTS (SELECT TOP 1 [Status] 
											 FROM dbo.TakenLessonPaidStatusLookupTable tlpsl 
											 WHERE tlpsl.Id = tl.TakenLessonPaidStatus) 
								THEN (SELECT [Status]
											 FROM dbo.TakenLessonPaidStatusLookupTable tlpsl 
											 WHERE tlpsl.Id = tl.TakenLessonPaidStatus) ELSE NULL END) AS TakenLessonPaidStatus
                            
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId  
                            INNER JOIN dbo.SubjectLookupTable slut ON slut.Id = tl.Subject
                        WHERE tl.TeacherId = @teacherId
                        AND DATEDIFF(day, tl.LessonDate, @date)=0
                        ";
            using (var connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(sql, new { teacherId = teacherId, date = date })
                    .Select(x =>
                    {

                        var takenLesson = new RegisterTakenLessonDto();
                        takenLesson.Id = x.Id;
                        takenLesson.StudentName = x.StudentName;
                        takenLesson.Hours = x.Hours;
                        takenLesson.TakenLessonPaidStatus = x.TakenLessonPaidStatus;
                        takenLesson.Status = (TakenLessonPaidStatus) x.Status;
                        takenLesson.Subject = x.Subject;
                        takenLesson.GroupFee = x.GroupFee;
                        takenLesson.GroupPay = x.GroupPay;
                        takenLesson.SingleFee = x.SingleFee;
                        takenLesson.SinglePay = x.SinglePay;
                        takenLesson.LessonType = x.LessonType;
                        takenLesson.StudentId = x.StudentId;
                        takenLesson.StripeInvoiceId = x.StripeInvoiceId;
                        takenLesson.StripeRefundId = x.StripeRefundId;
                        return takenLesson;
                    }

                     ).ToList();
            }
        }

        public IList<TakenLessonDto> GetTakenLessonDtos(int teacherId, DateTime date)
        {
            var sql = @"
                        SELECT 
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
                        WHERE tl.TeacherId = @teacherId
                        AND DATEDIFF(day, tl.LessonDate, @date)=0
                        ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new { teacherId = teacherId, date = date })
                    .Select(x =>
                    {

                        var takenLesson = new TakenLessonDto
                            (
                                x._id,
                                (Subject)x.Subject,
                                x.Teacher,
                                x.SingleFee,
                                x.SinglePay,
                                x.GroupFee,
                                x.GroupPay,
                                (LessonType)x.LessonType,
                                x.TeacherId,
                                (YearGroup)x.YearGroup,
                                x.TotalFee,
                                x.TotalPay,
                                x.LessonDate

                            );
                        takenLesson.StudentFirstName = x.StudentFirstName;
                        takenLesson.StudentId = x.StudentId;
                        takenLesson.Hours = x.Hours;
                        return takenLesson;
                    }

                     ).ToList();
            }
        }

        public IList<TakenLessonDto> GetUninvoicedTakenLessons(int parentId)
        {
            var sql = @"
                        SELECT 
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                        INNER JOIN dbo.Parent p ON p.Id = s.ParentId
                        WHERE p.Id = @parentId and tl.StripeInvoiceId is null
                        ";
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new { parentId = parentId })
                    .Select(x =>
                    {

                        var takenLesson = new TakenLessonDto
                            (
                                x._id,
                                (Subject)x.Subject,
                                x.Teacher,
                                x.SingleFee,
                                x.SinglePay,
                                x.GroupFee,
                                x.GroupPay,
                                (LessonType)x.LessonType,
                                x.TeacherId,
                                (YearGroup)x.YearGroup,
                                x.TotalFee,
                                x.TotalPay,
                                x.LessonDate

                            );
                        takenLesson.StudentFirstName = x.StudentFirstName;
                        takenLesson.StudentId = x.StudentId;
                        takenLesson.Hours = x.Hours;
                        takenLesson.LessonDate = x.LessonDate;
                        return takenLesson;
                    }

                     ).ToList();
            }
        }

        public int GetNumberOfPages(int? pageSize = 2)
        {
            var sql = "SELECT Ceiling(COUNT(ID)*1.0/@pageSize) from dbo.TakenLesson tl";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<int>(sql, new { pageSize = pageSize });
            }
        }

        public int GetTakenLessonsBetweenDatesNumberOfPages(DateTime? startDate, DateTime? endDate, 
            int? teacherId, int? parentId, int? studentId, int? invoiceId, string? stripeInvoiceId, string? stripeRefundId, int? pageSize = 2)
        {           

            var sql = @"
                        SELECT 
                            Ceiling(COUNT(tl.Id)*1.0/@pageSize)
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                        INNER JOIN dbo.Parent p ON p.Id = s.ParentId
                        WHERE 1=1 ";

            if (startDate != null)
            {
                sql += @"AND tl.LessonDate >= @startDate ";
            }

            if (endDate != null)
            {
                sql += @"AND tl.LessonDate <= @endDate ";
            }

            if (teacherId != null && teacherId > 0)
            {
                sql += @"AND tl.teacherId = @teacherId ";
            }

            if (studentId != null && studentId > 0)
            {
                sql += @"AND tl.studentId = @studentId ";
            }

            if (parentId != null && parentId > 0)
            {
                sql += @"AND p.Id = @parentId ";
            }

            if (invoiceId != null)
            {
                sql += @"AND tl.InvoiceId = @invoiceId ";
            }

            if (stripeInvoiceId != null)
            {
                sql += @"AND tl.StripeInvoiceId = @stripeInvoiceId ";
            }

            if (stripeRefundId != null)
            {
                sql += @"AND tl.StripeRefundId = @StripeRefundId ";
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<int>(sql, new
                {
                    startDate = startDate,
                    endDate = endDate,
                    teacherId = teacherId,
                    pageSize = pageSize,
                    studentId,
                    parentId,
                    invoiceId,
                    stripeInvoiceId,
                    stripeRefundId
                });
            }
        }

        public int GetTakenLessonsBetweenDatesCount(DateTime? startDate, DateTime? endDate, int? teacherId, int? parentId, int? studentId, int? invoiceId, string? stripeInvoiceId, string? stripeRefundId)
        {          

            var sql = @"
                        SELECT 
                            Count(tl.Id)
                        FROM dbo.TakenLesson tl
                        INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                    INNER JOIN dbo.Parent p ON p.Id = s.ParentId
                        
                        WHERE 1=1 ";

            if (startDate != null)
            {
                sql += @"AND tl.LessonDate >= @startDate ";
            }

            if (endDate != null)
            {
                sql += @"AND tl.LessonDate <= @endDate ";
            }

            if (teacherId != null)
            {
                sql += @"AND tl.teacherId = @teacherId ";
            }

            if (studentId != null && studentId > 0)
            {
                sql += @"AND tl.studentId = @studentId ";
            }

            if (parentId != null && parentId > 0)
            {
                sql += @"AND p.Id = @parentId ";
            }

            if (invoiceId != null)
            {
                sql += @"AND tl.InvoiceId = @invoiceId ";
            }

            if (stripeInvoiceId != null)
            {
                sql += @"AND tl.StripeInvoiceId = @stripeInvoiceId ";
            }

            if (stripeRefundId != null)
            {
                sql += @"AND tl.StripeRefundId = @StripeRefundId ";
            }


            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<int>(sql, new
                {
                    startDate = startDate,
                    endDate = endDate,
                    teacherId = teacherId,
                    parentId,
                    studentId,
                    invoiceId,
                    stripeInvoiceId,
                    stripeRefundId
                });
            }
        }

        public IList<TakenLessonPanelDto> GetTakenLessonsBetweenDates(DateTime? startDate, DateTime? endDate, int? teacherId,
            int? parentId, int? studentId, int? invoiceId, string? stripeInvoiceId, string? stripeRefundId, int? pageNumber = 1, int? pageSize  = 2) 
        {
            var offset = (pageNumber - 1) * pageSize;

            var sql = @"
                        SELECT 
                            tl.Id AS Id,
                            slut.Subject AS Subject,
                            s.FirstName AS Student,
                            t.FirstName AS Teacher,
                            tl.TotalPay AS TotalPay,
                            tl.TotalFee AS TotalFee,
                            tl.LessonDate AS LessonDate,
                            tl.InvoiceId AS InvoiceId,
                            tl.StripeInvoiceId AS StripeInvoiceId,
                            tl.StripeRefundId AS StripeRefundId,
                            tlpslut.Status AS Status,                    
                            tl.Hours AS Hours                    
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Teacher t ON t.Id = tl.TeacherId
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                        INNER JOIN dbo.Parent p ON p.Id = s.ParentId
                            INNER JOIN dbo.TakenLessonPaidStatusLookupTable tlpslut ON tlpslut.Id = tl.TakenLessonPaidStatus
                            INNER JOIN dbo.SubjectLookupTable slut ON slut.Id = tl.Subject
                        WHERE 1 = 1 ";

                        if (startDate != null) 
                        {
                            sql += @"AND tl.LessonDate >= @startDate ";
                        }

                        if (endDate != null)
                        {
                            sql += @"AND tl.LessonDate <= @endDate ";
                        }

                        if (teacherId != null && teacherId > 0)
                        {
                            sql += @"AND tl.teacherId = @teacherId ";
                        }

                        if (studentId != null && studentId > 0) 
                        {
                            sql += @"AND tl.studentId = @studentId ";
                        }

                        if (parentId != null && parentId > 0)
                        {
                            sql += @"AND p.Id = @parentId ";
                        }

                        if (invoiceId != null) 
                        {
                            sql += @"AND tl.InvoiceId = @invoiceId ";
                        }

                        if (stripeInvoiceId != null)
                        {
                            sql += @"AND tl.StripeInvoiceId = @stripeInvoiceId ";
                        }

                        if (stripeRefundId != null)
                        {
                            sql += @"AND tl.StripeRefundId = @stripeRefundId ";
                        }

                        sql += "ORDER BY tl.Id OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";

                        using (var connection = new SqlConnection(ConnectionString))
                        {
                            return connection.Query<TakenLessonPanelDto>(sql, new { startDate = startDate, endDate = endDate,
                               parentId, studentId, teacherId = teacherId, offset = offset, pageSize = pageSize,
                                invoiceId,
                                stripeInvoiceId,
                                stripeRefundId
                            }).ToList();
                        }
        }

        public IList<TakenLessonDto> GetTakenLessons(string? invoiceId, string? stripeInvoiceId, int? parentId)
        {
            string sql = "";
            if (invoiceId == null && stripeInvoiceId == null && parentId == null) 
            {
                //load 10 most recent invoices
                sql = @"SELECT TOP 10 
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate
                        FROM dbo.TakenLesson tl 
                            INNER JOIN dbo.Invoice i on i.StripeInvoiceId = tl.StripeInvoiceId
                            INNER JOIN dbo.Student s on s.Id = tl.StudentId
                            INNER JOIN dbo.Teacher t on t.Id = tl.TeacherId
                            INNER JOIN dbo.Parent p on p.Id = s.ParentId
                        ORDER BY tl.LessonDate DESC";
            }

            sql = @"SELECT 
                            tl.Id AS _id,
                            tl.Subject AS Subject,
                            t.FirstName AS Teacher,
                            tl.SingleFee AS SingleFee,
                            tl.GroupFee AS GroupFee,
                            tl.SinglePay AS SinglePay,
                            tl.GroupPay AS GroupPay,
                            tl.LessonType AS LessonType,
                            tl.TeacherId AS TeacherId,
                            tl.YearGroup AS YearGroup,
                            tl.TotalFee AS TotalFee,
                            tl.TotalPay AS TotalPay,
                            s.FirstName AS StudentFirstName,
                            tl.StudentId AS StudentId,
                            tl.Hours AS Hours,
                            tl.LessonDate AS LessonDate
                    FROM dbo.TakenLesson tl
                        INNER JOIN dbo.Invoice i on i.StripeInvoiceId = tl.StripeInvoiceId
                        INNER JOIN dbo.Student s on s.Id = tl.StudentId
                        INNER JOIN dbo.Teacher t on t.Id = tl.TeacherId
                        INNER JOIN dbo.Parent p on p.Id = s.ParentId
                    WHERE 1 = 1 ";

            if (stripeInvoiceId != null) 
            {
                sql += @"AND tl.StripeInvoiceId = @stripeInvoiceId ";
            }

            if (invoiceId != null)
            {
                sql += @"AND i.Id = @invoiceId ";
            }

            if (parentId != null)
            {
                sql += @"AND p.Id = @parentId";
            }


            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new { invoiceId = invoiceId, stripeInvoiceId = stripeInvoiceId, parentId = parentId })
                    .Select(x =>
                    {

                        var takenLesson = new TakenLessonDto
                            (
                                x._id,
                                (Subject)x.Subject,
                                x.Teacher,
                                x.SingleFee,
                                x.SinglePay,
                                x.GroupFee,
                                x.GroupPay,
                                (LessonType)x.LessonType,
                                x.TeacherId,
                                (YearGroup)x.YearGroup,
                                x.TotalFee,
                                x.TotalPay,
                                x.LessonDate

                            );
                        takenLesson.StudentFirstName = x.StudentFirstName;
                        takenLesson.StudentId = x.StudentId;
                        takenLesson.Hours = x.Hours;
                        takenLesson.LessonDate = x.LessonDate;
                        return takenLesson;
                    }

                     ).ToList();
            }
        }
    }
}
