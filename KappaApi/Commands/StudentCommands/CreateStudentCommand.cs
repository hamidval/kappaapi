using KappaApi.Models;
using KappaApi.Models.Api;

namespace KappaApi.Commands.StudentCommands
{
    public class CreateStudentCommand
    {
        public CreateStudentCommand(StudentApiModel student) 
        {
            Student = student;
        }
        public StudentApiModel Student { get; set; }
    }
}
