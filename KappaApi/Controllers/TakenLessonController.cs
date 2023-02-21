using AutoMapper;
using AutoMapper.Configuration.Annotations;
using KappaApi.Commands;
using KappaApi.Commands.TakenLessonCommands;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KappaApi.Controllers
{
    [Route("/api/taken-lesson")]
    [ApiController]
    public class TakenLessonController : ControllerBase
    {

        private readonly ITakenLessonQuery _takenLessonQuery;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;
        public TakenLessonController(ITakenLessonQuery takenLessonQuery, ICommandBus commandBus, IMapper mapper) 
        {
            _takenLessonQuery = takenLessonQuery;
            _commandBus = commandBus;
            _mapper = mapper;
        }

        [Route("/api/taken-lesson/add")]
        [HttpPost]
        public void AddTakenLesson(TakenLessonApiModel model) 
        {
            var command = new CreateTakenLessonCommand(model);
            _commandBus.SendAsync(command);

        }

        [Route("/api/taken-lesson/get/{teacherId}/{date}")]
        [HttpGet]
        public IList<TakenLessonDto> GetLessonsByTeacherAndDate(int teacherId, DateTime date) 
        {
                
            return _takenLessonQuery.GetTakenLessons(teacherId, date);           

        }



    }
}
