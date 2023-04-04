using KappaApi.Domain;
using KappaApi.Enums;

namespace KappaApi.Models.Api
{
    public class EditLessonApiModel : LessonApiModel
    {
        public EditLessonApiModel() { }
        public virtual int Id { get; set; }

    }
}
