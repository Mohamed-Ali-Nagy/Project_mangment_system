using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.UserVMs;

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

        [HttpPost("reset-password")]
        public async Task<ResultVM<bool>> ResetPassword([FromBody] ResetPasswordVM viewModel)
        {
            var command = MapperHelper.MapOne<ResetPasswordCommand>(viewModel);

            return await _mediator.Send(command);
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM verifyEmailVM)
        {
            var resultDTO = await _mediator.Send(verifyEmailVM.MapOne<VerifyOTPCommand>());
            if (!resultDTO.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(Enums.ErrorCode.UserEmailNotFound,resultDTO.Message));
            }
            return Ok(ResultVM<bool>.Sucess(true, "email verified successfully"));
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var result = await _mediator.Send(new ForgetPasswordCommand(forgetPasswordVM.Email));
            if (!result.IsSuccess)
            {
                return Ok (ResultVM<bool>.Faliure(ErrorCode.UserEmailNotFound, "Can not send message to this email"));
            }
            return Ok(ResultVM<bool>.Sucess(true, "An email sent with an otp"));
        }

        [HttpPost("login")]
        public async Task<ResultVM<string>> UserLogin(UserLoginVM viewModel)
        {
            var userDTO = MapperHelper.MapOne<UserLoginDTO>(viewModel);
            var data = await _mediator.Send(new UserLoginCommand(userDTO));
            return ResultVM<string>.Sucess(data);
            // return result;
        }
    }
}
