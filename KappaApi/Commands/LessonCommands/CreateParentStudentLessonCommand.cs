using KappaApi.Models;
using KappaApi.Models.Api;

namespace KappaApi.Commands.LessonCommands
{
    public class CreateParentStudentLessonCommand
    {
        public CreateParentStudentLessonCommand(ParentStudentLessonApiModel parentStudentLessonApiModel)
        {
            ParentStudentLessonApiModel = parentStudentLessonApiModel;
        }
        public ParentStudentLessonApiModel ParentStudentLessonApiModel { get; set; }
    }
}
