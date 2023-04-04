using NHibernate.Mapping.ByCode;

namespace KappaApi.Models.Api
{
    public class ParentStudentLessonApiModel
    {
        public string? ParentFirstName { get; set; }
        public string? ParentLastName { get; set; }
        public string? ParentEmail { get; set; }
        public IList<StudentLessonApiModel>? Students { get; set; }

    }



}
