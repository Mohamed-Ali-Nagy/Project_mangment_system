using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.User.Commands;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok("Password has been reset successfully.");
            }
            return BadRequest("Invalid token or error resetting password.");
        }
    }
}
