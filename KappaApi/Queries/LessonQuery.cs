using Dapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;

namespace KappaApi.Queries
{
    public class LessonQuery : ILessonQuery
    {
        private readonly string ConnectionString = DbConnectionFactory.ConnectionString;
        public LessonDto GetLessonById(int id)
        {
            var sql = @"SELECT 
                            l.Id As LessonId,
                            l.Subject as Subject,
                            l.YearGroup as YearGroup,l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,
                            l.LessonType as LessonType,
                            l.Day as Day,                                        
                            ldlt.Day as DayText, 
                            l.StartDate as StartDate,
                            l.EndDate as EndDate, 
                            s.Id as StudentId,
                            s.FirstName as StudentFirstName,
                            s.LastName as StudentLastName,
                            t.Id as TeacherId,
                            t.FirstName as TeacherFirstName,
                            t.LastName as TeacherLastName,
                            t.Email as TeacherEmail
                                
                        FROM dbo.Lesson l
                            INNER JOIN dbo.Student s on s.Id = l.StudentId
                            INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                            INNER JOIN dbo.LessonDayLookupTable ldlt on ldlt.Id = l.Day
                        WHERE l.Id = @lessonId";
            using (SqlConnection connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(
                    sql
                    , new { lessonId = id })
                    .Select(x => new LessonDto(x.LessonId, x.Subject,
                    (x.TeacherFirstName + " " + x.TeacherLastName),
                    x.StartDate, x.EndDate, x.Day, x.DayText,
                    x.SingleFee, x.SinglePay, x.Groupfee, x.GroupPay, x.LessonType, x.TeacherId,(YearGroup) x.YearGroup)
                    ).First();
            }
        }

        public IList<LessonDto> GetLessonByStudent(int id)
        {
            var sql = @"SELECT 
                            l.Id As LessonId,
                            l.Subject as Subject,
                            l.YearGroup as YearGroup,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,
                            l.LessonType as LessonType,
                            l.Day as Day,                                        
                            ldlt.Day as DayText,                                        
                            l.StartDate as StartDate,
                            l.EndDate as EndDate,                                
                            s.Id as StudentId,
                            s.FirstName as StudentFirstName,
                            s.LastName as StudentLastName,
                            t.Id as TeacherId,
                            t.FirstName as TeacherFirstName,
                            t.LastName as TeacherLastName,
                            t.Email as TeacherEmail
                                
                        FROM dbo.Lesson l
                            INNER JOIN dbo.Student s on s.Id = l.StudentId
                            INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                            INNER JOIN dbo.LessonDayLookupTable ldlt on ldlt.Id = l.Day
                        WHERE l.StudentId = @studentId";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new { studentId = id })
                    .Select(x => new LessonDto(x.LessonId, (Subject) x.Subject,
                    x.TeacherFirstName, x.StartDate, x.EndDate, x.Day, x.DayText,
                    x.SingleFee, x.SinglePay, x.GroupFee, x.GroupPay, (LessonType) x.LessonType, x.TeacherId, (YearGroup)x.YearGroup)
                    ).ToList();
            }
        }

        public IList<LessonDto> GetLessonByTeacherAndDate(int id, DateTime date) 
        {
            var day = date.DayOfWeek;
            var sql = @"SELECT 
                            l.Id As LessonId,
                            l.Subject as Subject,
                            l.YearGroup as YearGroup,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,
                            l.LessonType as LessonType,
                            l.StudentId as StudentId,
                            s.FirstName as StudentFirstName,
                            l.Day as Day,                                        
                            ldlt.Day as DayText,                                        
                            l.StartDate as StartDate,
                            l.EndDate as EndDate,
                            t.Id as TeacherId,
                            t.FirstName as TeacherFirstName
                        FROM dbo.Lesson l
                        INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                        INNER JOIN dbo.Student s on s.Id = l.StudentId
                        INNER JOIN dbo.LessonDayLookupTable ldlt on ldlt.Id = l.Day
                        WHERE l.TeacherId = @id and 
                            l.StartDate <= @date and
                            l.EndDate >= @date and
                            l.Day = @day";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new { id = id, date = date, day = day })
                    .Select(x => {

                        var lesson = new LessonDto(x.LessonId, (Subject)x.Subject,
                    x.TeacherFirstName, x.StartDate, x.EndDate, x.Day, x.DayText,
                    x.SingleFee, x.SinglePay, x.GroupFee, x.GroupPay, (LessonType) x.LessonType, x.TeacherId, (YearGroup) x.YearGroup);


                        lesson.StudentId = x.StudentId;
                        lesson.StudentFirstName = x.StudentFirstName;
                        return lesson;
                        
                        }
                    ).ToList();
            }

        }

        public IList<LessonDto> GetLessonsByIds(List<int> ids)
        {
            var sql = @"SELECT 
                            l.Id As LessonId,
                            l.Subject as Subject,
                            l.YearGroup as YearGroup,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,
                            l.LessonType as LessonType,
                            l.StudentId as StudentId,
                            s.FirstName as StudentFirstName,
                            l.Day as Day,                                        
                            ldlt.Day as DayText,                                        
                            l.StartDate as StartDate,
                            l.EndDate as EndDate,
                            t.Id as TeacherId,
                            t.FirstName as TeacherFirstName
                        FROM dbo.Lesson l
                        INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                        INNER JOIN dbo.Student s on s.Id = l.StudentId
                        INNER JOIN dbo.LessonDayLookupTable ldlt on ldlt.Id = l.Day
                        WHERE l.Id in @ids";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new { ids = ids })
                    .Select(x => {

                        var lesson = new LessonDto(x.LessonId, (Subject)x.Subject,
                    x.TeacherFirstName, x.StartDate, x.EndDate, x.Day, x.DayText,
                    x.SingleFee, x.YearGroup, x.GroupFee, x.GroupPay, (LessonType)x.LessonType, x.TeacherId,(YearGroup) x.YearGroup);


                        lesson.StudentId = x.StudentId;
                        lesson.StudentFirstName = x.StudentFirstName;
                        return lesson;

                    }
                    ).ToList();
            }

        }
    }
}
