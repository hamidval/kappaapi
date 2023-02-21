using System.ComponentModel.DataAnnotations;

namespace KappaApi.Models.Dtos
{
    public class StudentDto
    {
        public StudentDto(int id, string firstName, string lastName) 
        {
            _id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public int _id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
