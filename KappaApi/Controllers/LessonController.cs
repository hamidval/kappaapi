using AutoMapper;
using KappaApi.Commands;
using KappaApi.Commands.LessonCommands;
using KappaApi.Domain;
using KappaApi.Enums;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.LessonQuery;
using KappaApi.Services.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace KappaApi.Controllers
{
    [ApiController]
    [Route("/api/lesson")]
    public class LessonController : ControllerBase
    {

        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;
        private readonly ILessonQuery _lessonQuery;
        private readonly IHubContext<ChatHub> _hubContext;
        public LessonController(ICommandBus commandBus, IMapper mapper, ILessonQuery lessonQuery, IHubContext<ChatHub> hubContext) 
        {
            _commandBus = commandBus;
            _mapper = mapper;
            _lessonQuery = lessonQuery;
            _hubContext = hubContext;
        }

        [HttpGet("signal")]
        public void SendSignal() 
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "lessons logged", "otter");
        }

        [HttpPost("add")]
        public void AddLessons(LessonApiModel model)
        {
            var command = new CreateLessonCommand(model);

            _commandBus.SendAsync(command);
        }
        
        [HttpPost("edit")]
        public void EditLessons(EditLessonApiModel model)
        {
            var command = new EditLessonCommand();
            command.Lesson = model;

            _commandBus.SendAsync(command);
        }

                
        [HttpPost]
        public void AddLessons(ParentStudentLessonApiModel model) 
        {
            var command = new CreateParentStudentLessonCommand(model);

            _commandBus.SendAsync(command);        

        }

        [HttpGet("student/{studentId}")]
        public IList<LessonPanelLessonDto> GetLessonsByStudent(int studentId) 
        {
            return _lessonQuery.GetLessonByStudentId(studentId);
        }
        
        [HttpGet("{id}")]
        public LessonFormDto GetLessonsById(int id)
        {
            return _lessonQuery.GetLessonById(id);
        }
              
        [HttpGet("date/{date}/{teacherId}")]
        public IList<RegisterLessonDto> GetLessonsForRegister(int teacherId, DateTime date) 
        {
            return _lessonQuery.GetLessonByTeacherAndDate(teacherId, date);
        }
                
        [HttpGet("subjects")]
        public IList<SubjectDto> GetAllLessonSubjects() 
        {
            return _lessonQuery.GetAllLessonSubjects();
        }

        [HttpGet("yeargroups")]
        public IList<YearGroupDto> GetAllLessonYearGroups()
        {
            return _lessonQuery.GetAllLessonYearGroups();
        }


    }
}
