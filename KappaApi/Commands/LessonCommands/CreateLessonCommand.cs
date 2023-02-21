using KappaApi.Models;
using KappaApi.Models.Api;

namespace KappaApi.Commands.LessonCommands
{
    public class CreateLessonCommand
    {
        public CreateLessonCommand(LessonApiModel lesson) 
        {
            Lesson = lesson;
        }

        public LessonApiModel Lesson { get; set; }
    }
}
