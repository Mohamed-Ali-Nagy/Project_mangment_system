using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.User.Commands;
using Project_management_system.ViewModels;

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

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string email, string otpCode)
        {
            var isVerified = await _mediator.Send(new VerifyOTPCommand(email, otpCode));
            if (!isVerified)
            {
                return BadRequest("email is not verified");
            }
            return Ok(ResultVM<bool>.Sucess(true, "email verified successfully"));
        }
    }
}
