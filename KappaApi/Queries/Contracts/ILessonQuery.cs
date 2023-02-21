using KappaApi.Models;
using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface ILessonQuery
    {
        public LessonDto GetLessonById(int id);
        public IList<LessonDto> GetLessonByStudent(int studentId);

        public IList<LessonDto> GetLessonByTeacherAndDate(int id, DateTime date);
        public IList<LessonDto> GetLessonsByIds(List<int> ids);
    }
}
