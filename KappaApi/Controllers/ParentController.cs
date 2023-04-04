using KappaApi.Commands;
using KappaApi.Commands.ParentCommands;
using KappaApi.Models;
using KappaApi.Models.Api;
using KappaApi.Models.Dtos;
using KappaApi.NHibernateMappings;
using KappaApi.Queries.Contracts;
using KappaApi.Services.StripeService;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KappaApi.Controllers
{
    [ApiController]
    [Route("/api/parent")]
    [ApiVersion("1.0")]
    public class ParentController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        private readonly ILogger<ParentController> _logger;
        private readonly ICommandBus _commandBus;
        private readonly IParentQuery _parentQuery;

        public ParentController(ICommandBus commandBus, ILogger<ParentController> logger, 
            IStripeService stripeService, IParentQuery parentQuery)
        {
            _logger = logger;
            _stripeService = stripeService;
            _commandBus = commandBus;
            _parentQuery = parentQuery;
        }

        [HttpGet("get-data")]
        public string GetData()
        {
            return "version 1";
        }

        [HttpGet("check-email")]
        public bool CheckEmail(string email) 
        {
            return _parentQuery.CheckIfEmailExists(email);
        }
        
        [HttpPost]
        public IActionResult AddParent(ParentApiModel model)
        {
            var command = new CreateParentCommand();
            command.Parent = model;

            _commandBus.SendAsync(command);

            return StatusCode(200);
            
        }

        [HttpGet]
        public List<ParentDto> GetAllParents() 
        {
            return _parentQuery.GetAllParents();
        }

        [HttpGet("search/{searchTerm?}")]
        public List<ParentDto> GetAllParentsPagination(string? searchTerm)
        {
            return _parentQuery.GetAllParentsPaginatedSerach(searchTerm);
        }

        [HttpGet("{id}")]        
        public Parent? GetParentById(int id)
        {
            return _parentQuery.GetParentById(id);
        }

        [HttpPut("archive/{id}")]       
        public void ArchiveParent(int id) 
        {
            var command = new ArchiveParentCommand();
            command.ParentId = id;
            _commandBus.SendAsync(command);
        }
    }
}