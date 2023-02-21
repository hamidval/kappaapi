using KappaApi.Models;

namespace KappaApi.Commands.StudentCommands
{
    public class CreateStudentCommand
    {
        public CreateStudentCommand(Student student) 
        {
            Student = student;
        }
        public Student Student { get; set; }
    }
}
