using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface IStudentQuery
    {
        public IList<StudentDto> GetAllStudents(int id = 0);
    }
}
