using KappaApi.Commands;
using KappaApi.Commands.ParentCommands;
using KappaApi.Models;
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

        [Route("/api/parent/check-email")]
        [HttpGet]
        public bool CheckEmail(string email) 
        {
            return _parentQuery.CheckIfEmailExists(email);
        }

        [Route("/api/parent")]
        [HttpPost]
        public void AddParent()
        {
            var command = new CreateParentCommand();

            _commandBus.SendAsync(command);
            
        }

        [Route("/api/parent")]
        [HttpGet]
        public List<ParentDto> GetAllParents() 
        {
            return _parentQuery.GetAllParents();
        }
    }
}