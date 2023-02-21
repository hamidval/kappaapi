using Dapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace KappaApi.Queries
{
    public class TakenLessonQuery : BaseQuery, ITakenLessonQuery
    {
       
        public TakenLessonQuery() 
        {

        }
        public IList<TakenLessonDto> GetTakenLessons(int teacherId, DateTime date)
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
                            tl.Hours AS Hours
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
                                x.TotalPay

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
                                x.TotalPay

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
