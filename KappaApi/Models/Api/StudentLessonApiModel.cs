namespace KappaApi.Models.Api
{
    public class StudentLessonApiModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IList<LessonApiModel> Lessons {get; set;}

    }
}
