namespace KappaApi.Models
{
    public class Teacher
    {
        public Teacher() { }

        public virtual int Id { get; set; }
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
        public virtual string? Email { get; set; }
    }
}