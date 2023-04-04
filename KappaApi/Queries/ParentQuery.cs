using Dapper;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Mvc;
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
                            p.Email,
                            p.StripeCustomerId,
                            pslt.Status AS Status
                        FROM dbo.Parent p
                            INNER JOIN dbo.ParentStatusLookupTable pslt on pslt.Id = p.Status
                    ";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<ParentDto>(sql).ToList();
            }
        }

        public List<ParentDto> GetAllParentsPaginatedSerach(string? searchTerm)
        {
            var sql = @"";
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                sql = @"
                        SELECT TOP 3
                            p.Id,
                            p.FirstName,
                            p.LastName,
                            p.Email,
                            p.StripeCustomerId,
                            pslt.Status AS Status
                        FROM dbo.Parent p
                            INNER JOIN dbo.ParentStatusLookupTable pslt on pslt.Id = p.Status
                    ";
            }
            else 
            {
                sql = @"
                        SELECT 
                            p.Id,
                            p.FirstName,
                            p.LastName,
                            p.Email,
                            p.StripeCustomerId,
                            pslt.Status AS Status
                        FROM dbo.Parent p
                            INNER JOIN dbo.ParentStatusLookupTable pslt on pslt.Id = p.Status
                            WHERE p.FirstName like '%" + searchTerm + @"%'
                            OR LastName like '%" + searchTerm + @"%'
                            OR StripeCustomerId like '%" + searchTerm + @"%'";
            }            

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<ParentDto>(sql, new { searchTerm = searchTerm}).ToList();
            }
        }

        public Parent? GetParentByStudentId(int id)
        {
            var sql = @"
                        SELECT 
                            TOP 1
                            p.Id,
                            P.FirstName AS FirstName,
                            P.LastName AS LastName,
                            P.Email AS Email,
                            P.StripeCustomerId AS StripeCustomerId
                        FROM dbo.Parent p
                            INNER JOIN dbo.Student s on s.ParentId = p.Id
                        WHERE s.Id = @id
                    ";

            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<Parent>(sql, new { id = id }).FirstOrDefault();
            }
        }

        






    }
}
