using KappaApi.Commands;
using KappaApi.Commands.StudentCommands;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
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
        

        public  StudentController(ICommandBus commandBus, IStudentQuery studentQuery) 
        {
            _studentQuery = studentQuery;
            _commandBus = commandBus;
        }

        [HttpPost]
        [Route("/api/student")]
        public void AddStudent(Student student)
        {
            var command = new CreateStudentCommand(student);   
            
            _commandBus.SendAsync(command);
            
        }

        [HttpGet]
        [Route("/api/student")]
        public List<StudentDto> GetAllStudents() 
        {
            return _studentQuery.GetAllStudents().ToList();
        }

        [HttpGet]
        [Route("/api/student/{id}")]
        public List<StudentDto> GetAllStudents(int id)
        {
            return _studentQuery.GetAllStudents(id).ToList();
        }



    }
}
