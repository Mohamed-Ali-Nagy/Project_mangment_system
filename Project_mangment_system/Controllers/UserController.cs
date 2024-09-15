using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.UserVMs;
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

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult>ForgetPassword(string email)
        {
           var result= await  _mediator.Send(new  ForgetPasswordCommand(email));
            if(!result)
            {
                return BadRequest("Can not send otp to this email");
            }
            return Ok(ResultVM<bool>.Sucess(true, "An email sent with an otp"));
        }
        private readonly IMediator _mediator;
        IMapper _mapper;
        public UserController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task <ResultVM<string>> UserLogin(UserLoginVM viewModel)
        {
            var userDTO = MapperHelper.MapOne<UserLoginDTO>(viewModel);
           var data = await _mediator.Send(new UserLoginCommand(userDTO));
            return  ResultVM<string>.Sucess(data);
           // return result;
        }
    }
}
