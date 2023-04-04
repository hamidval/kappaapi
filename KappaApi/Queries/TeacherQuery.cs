using Dapper;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.TeacherQuery;
using Microsoft.Data.SqlClient;
using NHibernate;

namespace KappaApi.Queries
{
    public class TeacherQuery : ITeacherQuery
    {
        private readonly string ConnectionString = DbConnectionFactory.ConnectionString;
        public TeacherQuery() 
        {
          
        }

        public IList<TeacherDto> GetTeachers(int? id = null)
        {
            var sql = @"SELECT 
                            t.Id AS _Id,
                            t.FirstName AS FirstName,
                            t.LastName AS LastName,
                            t.Email AS Email
                        FROM dbo.Teacher t
                        WHERE 1=1 ";
            if (id != null)             
            {
                sql += @"AND t.Id = @id";
            }

            using (var connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(sql, new { id = id }).Select(x => new TeacherDto(x._Id, x.FirstName, x.LastName, x.Email)).ToList();
            }            
          
        }

        public IList<TeacherTakenLessonDto> GetTeachersForTakenLessonPanel()
        {
            var sql = @"select t.Id AS Id, (t.FirstName + ' ' + t.LastName) AS FullName from Teacher t";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<TeacherTakenLessonDto>(sql).ToList();
            }

        }

        public TeacherDto? GetTeacherByEmail(string email)
        {
            var sql = @"SELECT 
                            t.Id AS _Id,
                            t.FirstName AS FirstName,
                            t.LastName AS LastName,
                            t.Email AS Email
                        FROM dbo.Teacher t
                        WHERE t.email = @email";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new {email = email }).Select(x => new TeacherDto(x._Id, x.FirstName, x.LastName, x.Email)).ToList().FirstOrDefault();
            }

        }
    }
}
