using KappaApi.Models;
using KappaApi.Services.StripeService;
using Microsoft.AspNetCore.Mvc;

namespace KappaApi.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStripeService _stripeService;

        public WeatherForecastController( IStripeService stripeService)
        {
            
            _stripeService = stripeService;
        }
        [Route("help")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var service = _stripeService;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

      
    }
}