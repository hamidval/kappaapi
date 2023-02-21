using Dapper;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
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

        public IList<TeacherDto> GetTeachers()
        {
            var sql = @"SELECT 
                            t.Id AS _Id,
                            t.FirstName AS FirstName,
                            t.LastName AS LastName
                        FROM dbo.Teacher t";

            using (var connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(sql).Select(x => new TeacherDto(x._Id, x.FirstName, x.LastName)).ToList();
            }
            
          
        }
    }
}
