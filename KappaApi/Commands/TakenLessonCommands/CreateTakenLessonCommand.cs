using KappaApi.Models;
using KappaApi.Models.Api;

namespace KappaApi.Commands.TakenLessonCommands
{
    public class CreateTakenLessonCommand
    {
        public CreateTakenLessonCommand(TakenLessonApiModel model) 
        {
            TakenLessonApiModel = model;
        }
        public TakenLessonApiModel TakenLessonApiModel { get; set; }

    }
}
