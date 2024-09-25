using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_management_system.Enums;
using Project_management_system.Repositories;
using Project_management_system.ViewModels;
using Entity = Project_management_system.Models;

namespace Project_management_system.CQRS.Users.Commands
{
    public record ResetPasswordCommand(string Email, string Otp, string NewPassword, string ConfirmPassword) : IRequest<ResultVM<bool>>;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultVM<bool>>
    {
        private readonly IBaseRepository<Entity.User> _userRepository;
        private readonly IPasswordHasher<Entity.User> _passwordHasher;

        public ResetPasswordCommandHandler(IBaseRepository<Entity.User> userRepository, IPasswordHasher<Entity.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResultVM<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return ResultVM<bool>.Faliure(ErrorCode.PasswordDontMatched, "Passwords do not match");
            }

            var user = await _userRepository.Get(u => u.Email == request.Email).FirstOrDefaultAsync();
            if (user is null || user.Otp != request.Otp || user.OtpExpiry < DateTime.Now)
            {
                return ResultVM<bool>.Faliure(ErrorCode.InvalidOTP, "Invalid otp or error resetting password.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);  //_passwordHasher.HashPassword(user, request.NewPassword);
            user.Otp = null;
            user.OtpExpiry = null;

            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return ResultVM<bool>.Sucess(true, "Password has been reset successfully.");
        }
    }
}
