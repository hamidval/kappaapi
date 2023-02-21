using Dapper;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.Data.SqlClient;
using NHibernate;

namespace KappaApi.Queries
{
    public class ParentQuery: BaseQuery , IParentQuery
    {
        public ParentQuery() 
        {
            
        }
        public bool CheckIfEmailExists(string email)
        {
            var sql = @"SELECT 1
                        FROM Parent
                        WHERE Email = @email;";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<bool>(sql, new { email = email }).FirstOrDefault();
            }
        }

        public List<int> GetParentsFromTakenLessons()
        {
            var sql = @"                        
                        SELECT DISTINCT p.Id AS ParentId
                        FROM dbo.TakenLesson tl
                            INNER JOIN dbo.Student s ON s.Id = tl.StudentId
	                        INNER JOIN dbo.Parent p ON s.ParentId = p.Id
                        WHERE tl.StripeInvoiceId is null and p.StripeCustomerId is not null

                        ";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<int>(sql).ToList();
            }
        }

        public Parent? GetParentById(int id) 
        {
            var sql = @"
                        SELECT 
                            *
                        FROM dbo.Parent p
                        WHERE p.Id = @id
                    ";

            using (var connection = new SqlConnection(ConnectionString)) 
            {
                return connection.Query<Parent>(sql, new { id = id}).FirstOrDefault();
            }
        }
        public List<ParentDto> GetAllParents()
        {
            var sql = @"
                        SELECT 
                            p.Id,
                            p.FirstName,
                            p.LastName,
                            p.Email
                        FROM dbo.Parent p
                    ";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<ParentDto>(sql).ToList();
            }
        }




    }
}
