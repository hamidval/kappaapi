using AutoMapper;
using KappaApi.Commands;
using KappaApi.Commands.LessonCommands;
using KappaApi.Domain;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KappaApi.Controllers
{
    [ApiController]
    [Route("/api/lesson")]
    public class LessonController : ControllerBase
    {

        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;
        private readonly ILessonQuery _lessonQuery;
        public LessonController(ICommandBus commandBus, IMapper mapper, ILessonQuery lessonQuery) 
        {
            _commandBus = commandBus;
            _mapper = mapper;
            _lessonQuery = lessonQuery;
        }

        [Route("/api/lesson/add")]
        [HttpPost]
        public void AddLessons(LessonApiModel model)
        {
            var command = new CreateLessonCommand(model);

            _commandBus.SendAsync(command);

        }


        [Route("/api/lesson")]
        [HttpPost]
        public void AddLessons(ParentStudentLessonApiModel model) 
        {
            var command = new CreateParentStudentLessonCommand(model);

            _commandBus.SendAsync(command);        

        }

        [Route("/api/lesson/student/{studentId}")]
        [HttpGet]
        public IList<LessonDto> GetLessonsByStudent(int studentId) 
        {
            return _lessonQuery.GetLessonByStudent(studentId);
        }

        [Authorize]
        [Route("/api/lesson/date/{date}/{teacherId}")]
        [HttpGet]
        public IList<LessonDto> GetLessonsForRegister(int teacherId, DateTime date) 
        {
            return _lessonQuery.GetLessonByTeacherAndDate(teacherId, date); 

        }

    }
}
