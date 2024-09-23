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
    public class UserController : BaseController
    {
        public UserController(ControllerParameters controllerParameters) : base(controllerParameters)
        {
        }

        [HttpPost("reset-password")]
        public async Task<ResultVM<bool>> ResetPasswordAsync([FromBody] ResetPasswordVM viewModel)
        {
            var command = MapperHelper.MapOne<ResetPasswordCommand>(viewModel);

            return await mediator.Send(command);
        }

        [HttpPost("change-password")]
        //[Authorize]
        public async Task<ResultVM<bool>> ChangePasswordAsync([FromBody] ChangePasswordVM viewModel)
        {
            var command = MapperHelper.MapOne<ChangePasswordCommand>(viewModel);

            return await mediator.Send(command);
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM verifyEmailVM)
        {
            var resultDTO = await mediator.Send(verifyEmailVM.MapOne<VerifyOTPCommand>());
            if (!resultDTO.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(Enums.ErrorCode.UserEmailNotFound, resultDTO.Message));
            }
            return Ok(ResultVM<bool>.Sucess(true, "email verified successfully"));
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            var result = await mediator.Send(new ForgetPasswordCommand(forgetPasswordVM.Email));
            if (!result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(ErrorCode.UserEmailNotFound, "Can not send message to this email"));
            }
            return Ok(ResultVM<bool>.Sucess(true, "An email sent with an otp"));
        }

        [HttpPost("login")]
        public async Task<ResultVM<string>> UserLogin(UserLoginVM userVM)
        {
            var result = await mediator.Send(userVM.MapOne<UserLoginCommand>());
            if (!result.IsSuccess)
            {
                return ResultVM<string>.Faliure(ErrorCode.WrongPasswordOrEmail, result.Message);
            }
            return ResultVM<string>.Sucess(result.Data, "User logged successfully");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var te = userRegisterVM.MapOne<UserRegisterCommand>();
            var result = await mediator.Send(userRegisterVM.MapOne<UserRegisterCommand>());
            if (!result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(ErrorCode.EmailIsNotUnique, result.Message));
            }
            return Ok(ResultVM<bool>.Sucess(true, "Registered successfully"));
        }


    }
}
