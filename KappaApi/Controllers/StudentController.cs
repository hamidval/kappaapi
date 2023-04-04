using KappaApi.Commands;
using KappaApi.Commands.StudentCommands;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.StudentQuery;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace KappaApi.Controllers
{
    [ApiController]
    [Route("/api/student")]
    public class StudentController : ControllerBase
    {

        private readonly ICommandBus _commandBus;
        private readonly IStudentQuery _studentQuery;


        public StudentController(ICommandBus commandBus, IStudentQuery studentQuery)
        {
            _studentQuery = studentQuery;
            _commandBus = commandBus;
        }

        

        [HttpPost]
        public void AddStudent(StudentApiModel student)
        {
            var command = new CreateStudentCommand(student);

            _commandBus.SendAsync(command);
        }

        [HttpGet("api/students")]
        public List<StudentDto> GetAllStudents()
        {
            return _studentQuery.GetAllStudentsById().ToList();
        }

        [HttpGet("{id}")]
        public StudentDto? GetStudent(int id)
        {
            return _studentQuery.GetAllStudentsById(id).FirstOrDefault();
        }

        [HttpGet("all")]
        public List<StudentPanelDto> GetAllStudentsForPanel()
        {
            return _studentQuery.GetAllStudents().ToList();
        }

        [HttpGet("/api/students/{id}")]
        public List<StudentDto> GetAllStudents(int id)
        {
            return _studentQuery.GetAllStudentsById(id).ToList();
        }

        [HttpDelete("{id}")]
        public void DeleteStudent(int id)
        {
            var command = new ArchiveStudentCommand();
            command.StudentId = id;
            _commandBus.SendAsync(command);

        }

        [HttpGet("search/{searchString?}")]
        public List<StudentDto> GetStudentsBySearchString(string? searchString) 
        {
            var students = new List<StudentDto>();
            if (searchString == "all" || searchString == null)
            {
                return _studentQuery.GetAllStudentsById().ToList();
            }            
            
            students =  _studentQuery.GetStudentsBySearchString(searchString).ToList();

            return students;
        }



    }
}
