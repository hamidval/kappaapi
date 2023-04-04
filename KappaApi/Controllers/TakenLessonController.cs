using AutoMapper;
using AutoMapper.Configuration.Annotations;
using KappaApi.Commands;
using KappaApi.Commands.TakenLessonCommands;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.Queries.Contracts;
using KappaApi.Queries.Dtos.TakenLessonQuery;
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

        [HttpGet("pages/{pageSize?}")]
        public int Pages(int? pageSize) 
        {
            return _takenLessonQuery.GetNumberOfPages(pageSize);
        }

        [HttpPost("add")]
        public void AddTakenLesson(TakenLessonApiModel model) 
        {
            var command = new CreateTakenLessonCommand(model);
            _commandBus.SendAsync(command);
        }

        [HttpGet("get/{teacherId}/{date}")]
        public IList<RegisterTakenLessonDto> GetLessonsByTeacherAndDate(int teacherId, DateTime date) 
        {                
            return _takenLessonQuery.GetTakenLessons(teacherId, date);      
        }

        [HttpGet("between")]
        public IList<TakenLessonPanelDto> GetTakenLessonsBetweenDates(
            [FromQuery(Name = "fromDate")] DateTime? fromDate,
            [FromQuery(Name = "toDate")]  DateTime? toDate,
            [FromQuery(Name = "teacherId")] int? teacherId,
            [FromQuery(Name = "pageNumber")] int? pageNumber,
            [FromQuery(Name = "pageSize")] int? pageSize,
            [FromQuery(Name = "studentId")] int? studentId,
            [FromQuery(Name = "parentId")] int? parentId,
            [FromQuery(Name = "invoiceId")] int? invoiceId,
            [FromQuery(Name = "stripeInvoiceId")] string? stripeInvoiceId,
            [FromQuery(Name = "stripeRefundId")] string? stripeRefundId
            ) 
        {
            return _takenLessonQuery.GetTakenLessonsBetweenDates(fromDate, toDate, teacherId, 
                parentId, studentId, invoiceId, stripeInvoiceId, stripeRefundId, pageNumber, pageSize);
        }

        [HttpGet("between/count")]
        public int Count(
            [FromQuery(Name = "fromDate")] DateTime? fromDate,
            [FromQuery(Name = "toDate")] DateTime? toDate,
            [FromQuery(Name = "teacherId")] int? teacherId,
            [FromQuery(Name = "studentId")] int? studentId,
            [FromQuery(Name = "parentId")] int? parentId,
            [FromQuery(Name = "invoiceId")] int? invoiceId,
            [FromQuery(Name = "stripeInvoiceId")] string? stripeInvoiceId,
            [FromQuery(Name = "stripeRefundId")] string? stripeRefundId)
        {
            return _takenLessonQuery.GetTakenLessonsBetweenDatesCount(fromDate, toDate, teacherId, parentId,
                studentId, invoiceId, stripeInvoiceId, stripeRefundId);
        }
                
        [HttpGet("between/pages")]
        public int Pages(
            [FromQuery(Name = "fromDate")] DateTime? fromDate,
            [FromQuery(Name = "toDate")] DateTime? toDate,
            [FromQuery(Name = "teacherId")] int? teacherId,
            [FromQuery(Name = "pageSize")] int? pageSize,
            [FromQuery(Name = "studentId")] int? studentId,
            [FromQuery(Name = "parentId")] int? parentId,
            [FromQuery(Name = "invoiceId")] int? invoiceId,
            [FromQuery(Name = "stripeInvoiceId")] string? stripeInvoiceId,
            [FromQuery(Name = "stripeRefundId")] string? stripeRefundId)
        {
            return _takenLessonQuery.GetTakenLessonsBetweenDatesNumberOfPages(fromDate, toDate, teacherId, parentId, studentId, invoiceId, stripeInvoiceId, stripeRefundId, pageSize);
        }

        [HttpGet("query")]
        public IList<TakenLessonDto> QueryTakenLesson(string? invoiceId, string? stripeInvoiceId , int? parentId)         
        {
            return _takenLessonQuery.GetTakenLessons(invoiceId, stripeInvoiceId, parentId);
        }
                
        [HttpPut("action/{command}/{takenLessonId}")]
        public void ActionTakenLesson(int command, int takenLessonId)
        {

            if (command == 0)
            {
                //refund

                var c = new RefundTakenLessonCommand();
                c.TakenLessonId = takenLessonId;
                _commandBus.SendAsync(c);

            }
            else 
            {
            }
        }

    }
}
