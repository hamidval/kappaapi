using KappaApi.Models.Dtos;
using KappaApi.Queries.Dtos.TeacherQuery;

namespace KappaApi.Queries.Contracts
{
    public interface ITeacherQuery
    {

        public IList<TeacherDto> GetTeachers(int? id = null);
        public TeacherDto? GetTeacherByEmail(string email);
        public IList<TeacherTakenLessonDto> GetTeachersForTakenLessonPanel();

    }
}
