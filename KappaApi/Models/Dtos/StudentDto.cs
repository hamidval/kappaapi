using KappaApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace KappaApi.Models.Dtos
{
    public class StudentDto
    {
        public StudentDto(int id, string firstName, string lastName, StudentStatus status) 
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Status = status;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public StudentStatus Status { get; set; }

    }
}
