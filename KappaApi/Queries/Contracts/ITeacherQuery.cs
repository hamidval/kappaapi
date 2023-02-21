using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface ITeacherQuery
    {

        public IList<TeacherDto> GetTeachers();
    }
}
