using KappaApi.Commands;
using KappaApi.Commands.TeacherCommands;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KappaApi.Controllers
{
    
    [ApiController]
    [Route("/api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ITeacherQuery _teacherQuery;
        public TeacherController(ICommandBus commandBus, ITeacherQuery  teacherQuery) 
        {
            _commandBus = commandBus;
            _teacherQuery = teacherQuery;
        }

        [HttpPost]
        [Route("/api/teacher")]
        public void AddTeacher(Teacher teacher) 
        {
            var command = new CreateTeacherCommand(teacher);
            _commandBus.SendAsync(command);
        }

        [Authorize(Policy = "ReadMessages")]
        [HttpGet]
        [Route("/api/teacher")]        
        public IList<TeacherDto> GetAllTeachers() 
        {
            return _teacherQuery.GetTeachers();
        }

        
    }
}
