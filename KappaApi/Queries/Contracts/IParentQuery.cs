using KappaApi.Models;
using KappaApi.Models.Dtos;

namespace KappaApi.Queries.Contracts
{
    public interface IParentQuery
    {
        public bool CheckIfEmailExists(string email);

        public List<int> GetParentsFromTakenLessons();

        public Parent? GetParentById(int id);

        public Parent? GetParentByStudentId(int id);

        public List<ParentDto> GetAllParents();

        public List<ParentDto> GetAllParentsPaginatedSerach(string? searchTerm);
    }
}
