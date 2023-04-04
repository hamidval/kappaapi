using KappaApi.Enums;

namespace KappaApi.Models
{
    public class Student
    {
        public Student(Parent parent) 
        {
            Parent = parent;
        }
        public Student() { }
        public virtual int Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }

        public virtual StudentStatus Status { get; set; }
        public virtual Parent? Parent { get; set; }
    }
}
