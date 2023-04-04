using KappaApi.Models.Dtos;
using KappaApi.Queries.Dtos.StudentQuery;

namespace KappaApi.Queries.Contracts
{
    public interface IStudentQuery
    {
        public IList<StudentDto> GetAllStudentsById(int id = 0);
        public IList<StudentPanelDto> GetAllStudents();
        public IList<StudentDto> GetStudentsBySearchString(string searchString);
        public IList<StudentDto> GetStudentsByParentId(int id);

    }
}
