using Dapper;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.LessonQuery;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;

namespace KappaApi.Queries
{
    public class LessonQuery : ILessonQuery
    {
        private readonly string ConnectionString = DbConnectionFactory.ConnectionString;


        public IList<SubjectDto> GetAllLessonSubjects() 
        {
            var sql = @"SELECT 
                            *                                
                        FROM dbo.SubjectLookupTable slut";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<SubjectDto>(sql).ToList();
            }
        }

        public IList<YearGroupDto> GetAllLessonYearGroups()
        {
            var sql = @"SELECT * FROM YearGroupLookupTable";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<YearGroupDto>(sql).ToList();
            }
        }

        public LessonFormDto GetLessonById(int id)
        {
            var sql = @"SELECT TOP 1
                            l.Id As Id,
                            l.StudentId as StudentId,
                            l.TeacherId as TeacherId,
                            l.Subject as Subject,
                            l.YearGroup as YearGroup,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,                                        
                            l.StartDate as StartDate,
                            l.EndDate as EndDate,
                            l.Day as Day 
                        FROM dbo.Lesson l
                        WHERE l.Id = @id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<LessonFormDto>(sql, new { id = id });
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

        public IList<LessonPanelLessonDto> GetLessonByStudentId(int id)
        {
            var sql = @"SELECT 
                            l.Id As Id,
                            s.FirstName as StudentName,
                            t.FirstName as TeacherName,
                            slut.Subject as Subject,
                            yglut.YearGroup as YearGroup,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,                                        
                            l.StartDate as StartDate,
                            l.EndDate as EndDate,
                            ldlut.Day as Day 
                        FROM dbo.Lesson l
                            INNER JOIN dbo.Student s on s.Id = l.StudentId
                            INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                            INNER JOIN dbo.SubjectLookupTable slut on slut.Id = l.Subject
                            INNER JOIN dbo.YearGroupLookupTable yglut on yglut.Id = l.YearGroup
                            INNER JOIN dbo.LessonDayLookupTable ldlut on ldlut.Id = l.Day

                        WHERE l.StudentId = @studentId";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<LessonPanelLessonDto>(sql, new { studentId = id }).ToList();
            }
        }

        public IList<RegisterLessonDto> GetLessonByTeacherAndDate(int id, DateTime date) 
        {
            var day = date.DayOfWeek;
            var sql = @"SELECT
                            l.Id As LessonId,                                                      
                            l.GroupFee as GroupFee,
                            l.GroupPay as GroupPay,
                            l.SingleFee as SingleFee,
                            l.SinglePay as SinglePay,
                            l.StudentId as StudentId,
                            s.FirstName as StudentName,                            
                            slut.Subject as Subject,
                            l.YearGroup as YearGroupId,
                            l.Subject as SubjectId
                        FROM dbo.Lesson l
                            INNER JOIN dbo.Student s on s.Id = l.StudentId
                            INNER JOIN dbo.SubjectLookupTable slut on slut.Id = l.Subject 
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

                        var lesson = new RegisterLessonDto();
                        lesson.Subject = x.Subject;
                        lesson.StudentId = x.StudentId;
                        lesson.StudentName = x.StudentName;
                        lesson.GroupFee = x.GroupFee;
                        lesson.GroupPay = x.GroupPay;
                        lesson.SingleFee = x.SingleFee;
                        lesson.SinglePay = x.SinglePay;
                        lesson.LessonId = x.LessonId;
                        lesson.SubjectId = x.SubjectId;
                        lesson.YearGroupId = x.YearGroupId;
                        
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

        public IList<LessonDto> GetLessonByParent(int id)
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
                            INNER JOIN dbo.Parent p on p.Id = s.ParentId
                            INNER JOIN dbo.Teacher t on t.Id = l.TeacherId
                            INNER JOIN dbo.LessonDayLookupTable ldlt on ldlt.Id = l.Day
                        WHERE s.StudentId = @id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(
                    sql
                    , new { id = id })
                    .Select(x => new LessonDto(x.LessonId, (Subject)x.Subject,
                    x.TeacherFirstName, x.StartDate, x.EndDate, x.Day, x.DayText,
                    x.SingleFee, x.SinglePay, x.GroupFee, x.GroupPay, (LessonType)x.LessonType, x.TeacherId, (YearGroup)x.YearGroup)
                    ).ToList();
            }
        }
    }
}
