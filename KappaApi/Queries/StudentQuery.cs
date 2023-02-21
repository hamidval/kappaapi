using Dapper;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.Data.SqlClient;

namespace KappaApi.Queries
{
    public class StudentQuery : BaseQuery, IStudentQuery
    {
        public IList<StudentDto> GetAllStudents(int id = 0)
        {
            var sql = @"
                        SELECT 
                            s.Id,
                            s.FirstName,
                            s.LastName
                        FROM dbo.Student s
                        ";

            if (id > 0) 
            {
                sql += @" WHERE s.Id = @id";
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(sql, new { id = id })
                    .Select(x => new StudentDto(x.Id, x.FirstName, x.LastName)).ToList();
            }
        }


    }
}
