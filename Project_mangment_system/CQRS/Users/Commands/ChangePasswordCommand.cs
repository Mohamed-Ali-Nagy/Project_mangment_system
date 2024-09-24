using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Enums;
using Project_management_system.Models;
using Project_management_system.ViewModels;

namespace Project_management_system.CQRS.Users.Commands
{
    public record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmPassword) : IRequest<ResultVM<bool>>;

    public class ChangePasswordCommandHandler : BaseRequestHandler<User, ChangePasswordCommand, ResultVM<bool>>
    {
        public ChangePasswordCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultVM<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(_userState.Id));
            if (!user.IsSuccess)
                return ResultVM<bool>.Faliure(ErrorCode.UserNotFound, "User not found");

            if (request.NewPassword != request.ConfirmPassword)
                return ResultVM<bool>.Faliure(ErrorCode.PasswordDontMatched, "Passwords do not match");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Data.Password);
            if (!isPasswordValid)
                return ResultVM<bool>.Faliure(ErrorCode.WrongOldPassword, "Old password is wrong");

            user.Data.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            _repository.Update(user.Data);
            _repository.SaveChanges();
            return ResultVM<bool>.Sucess(true, "Password has been changed successfully.");
        }
    }
}
