using Microsoft.AspNetCore.Mvc;
using Project_management_system.Helpers;

namespace Project_management_system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
   

        [HttpPost("send")]
        public async Task<IActionResult> SendMail(string ToEmail, string Subject, string Body)
        {
            await EmailService.SendEmailAsync(ToEmail, Subject, Body);
            return Ok();
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            
        }
    }

}    
