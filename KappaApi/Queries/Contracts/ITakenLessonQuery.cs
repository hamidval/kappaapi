using KappaApi.Models.Api;
using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface ITakenLessonQuery
    {

        public IList<TakenLessonDto> GetTakenLessons(int teacherId, DateTime date);

        public IList<TakenLessonDto> GetUninvoicedTakenLessons(int parentId);
    }
}
