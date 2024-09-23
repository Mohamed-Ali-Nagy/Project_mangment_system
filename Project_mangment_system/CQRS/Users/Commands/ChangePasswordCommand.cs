using MediatR;
using Microsoft.AspNetCore.Identity;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Enums;
using Project_management_system.Models;
using Project_management_system.ViewModels;

namespace Project_management_system.CQRS.Users.Commands
{
    public record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmPassword) : IRequest<ResultVM<bool>>;

    public class ChangePasswordCommandHandler : BaseRequestHandler<User, ChangePasswordCommand, ResultVM<bool>>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public ChangePasswordCommandHandler(RequestParameters<User> requestParameters, IPasswordHasher<User> passwordHasher) : base(requestParameters)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<ResultVM<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(_userState.Id));
            if (!user.IsSuccess)
                return ResultVM<bool>.Faliure(ErrorCode.UserNotFound, "User not found");

            if (request.NewPassword != request.ConfirmPassword)
                return ResultVM<bool>.Faliure(ErrorCode.PasswordDontMatched, "Passwords do not match");

            var result = BCrypt.Net.BCrypt.Verify( request.OldPassword, user.Data.Password); // _passwordHasher.VerifyHashedPassword(user.Data, user.Data.Password, request.OldPassword);
            if (!result) {
                return ResultVM<bool>.Faliure(ErrorCode.WrongOldPassword, "Old password is wrong"); 
            }


            user.Data.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);//_passwordHasher.HashPassword(user.Data, request.NewPassword);
            _repository.Update(user.Data);
            _repository.SaveChanges();
            return ResultVM<bool>.Sucess(true, "Password has been changed successfully.");
        }
    }
}
