using KappaApi.Models;

namespace KappaApi.Commands.TeacherCommands
{
    public class CreateTeacherCommand
    {

        public CreateTeacherCommand(Teacher teacher) 
        {
            Teacher = teacher;
        }

        public Teacher Teacher { get; set; }
    }
}
