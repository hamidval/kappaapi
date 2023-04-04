using KappaApi.Commands;
using KappaApi.Commands.TeacherCommands;
using KappaApi.Models;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.TeacherQuery;
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
        public void AddTeacher(Teacher teacher) 
        {
            var command = new CreateTeacherCommand(teacher);
            _commandBus.SendAsync(command);
        }

        
        [HttpGet("/api/teachers")]        
        public IList<TeacherDto> GetAllTeacherstan() 
        {
            return _teacherQuery.GetTeachers();
        }

        [HttpGet("all")]        
        public IList<TeacherTakenLessonDto> GetAllTeachers()
        {
            return _teacherQuery.GetTeachersForTakenLessonPanel();
        }

        [HttpGet("email/{email}")]
        public TeacherDto? GetTeacherByEmail(string email) 
        {
            return _teacherQuery.GetTeacherByEmail(email);
        }

        
    }
}
