using Dapper;
using KappaApi.Enums;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.StudentQuery;
using Microsoft.Data.SqlClient;

namespace KappaApi.Queries
{
    public class StudentQuery : BaseQuery, IStudentQuery
    {
        public IList<StudentDto> GetAllStudentsById(int id = 0)
        {
            var sql = @"
                        SELECT 
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            s.Status
                        FROM dbo.Student s
                        WHERE 1=1 AND s.Status = 1
                        ";

            if (id > 0) 
            {
                sql += @" AND s.Id = @id";
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query(sql, new { id = id })
                    .Select(x => new StudentDto(x.Id, x.FirstName, x.LastName, (StudentStatus)x.Status)).ToList();
            }
        }

        public IList<StudentPanelDto> GetAllStudents()
        {
            var sql = @"
                        SELECT 
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            sslu.Status
                        FROM dbo.Student s
                            INNER JOIN StudentStatusLookupTable sslu on sslu.ID = s.Status";
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql)
                    .Select(x =>
                    {
                        var student = new StudentPanelDto();
                        student.Id = x.Id;
                        student.FirstName = x.FirstName;
                        student.LastName = x.LastName;
                        student.Status = x.Status;

                        return student;

                    }).ToList();
            }
        }


        public IList<StudentDto> GetStudentsBySearchString(string searchString) 
        {
            var sql = "";
            if (string.IsNullOrWhiteSpace(searchString))
            {
                sql = @"
                        SELECT TOP 10
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            s.Status
                        FROM dbo.Student s
                        ";
            }
            else 
            {
                var search = searchString.Replace("[", "[[]").Replace("%", "[%]");
                var term = "%" + search + "%";
                sql = @"
                        SELECT 
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            s.Status
                        FROM dbo.Student s
                            INNER JOIN dbo.Parent p on p.Id = s.ParentId
                        WHERE s.Status = 1 AND
                            s.FirstName like  '%" + term + @"%' 
                                OR
                            s.LastName like '%" + term + @"%' 
                                OR
                            p.Id like '%" + term +  @"%'";
                            
                                
                        
            }

            

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new { term = searchString })
                    .Select(x => new StudentDto(x.Id, x.FirstName, x.LastName, (StudentStatus)x.Status)).ToList();
            }
        }

        public IList<StudentDto> GetStudentsByParentId(int id)
        {            
            var sql = @"
                        SELECT 
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            s.Status
                        FROM dbo.Student s
                            INNER JOIN dbo.Parent p on p.Id = s.ParentId
                        WHERE s.ParentId = @id                            
                        ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, new { id = id })
                    .Select(x => new StudentDto(x.Id, x.FirstName, x.LastName, (StudentStatus) x.Status)).ToList();
            }
        }

    }
}
