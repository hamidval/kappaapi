namespace KappaApi.Models.Dtos
{
    public class TeacherDto
    {
        public TeacherDto(int id, string firstName, string lastName) 
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
