using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Dtos.LessonQuery;

namespace KappaApi.Queries.Contracts
{
    public interface ILessonQuery
    {
        public LessonFormDto GetLessonById(int id);
        public IList<LessonDto> GetLessonByStudent(int studentId);
        public IList<LessonDto> GetLessonByParent(int parentId);

        public IList<RegisterLessonDto> GetLessonByTeacherAndDate(int id, DateTime date);
        public IList<LessonDto> GetLessonsByIds(List<int> ids);

        public IList<SubjectDto> GetAllLessonSubjects();
        public IList<YearGroupDto> GetAllLessonYearGroups();
        public IList<LessonPanelLessonDto> GetLessonByStudentId(int id);
    }
}
