using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Enums;
using Project_management_system.Exceptions;
using Project_management_system.Helpers;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(string email) : IRequest<bool>
    {
    }
    public record ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Models.User> _userRepository;
        public ForgetPasswordHandler(IMediator mediator, IBaseRepository<Models.User> baseRepository)
        {
            _userRepository = baseRepository;
            _mediator = mediator;
        }
        public async Task<bool> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            if (user == null)
            {
                throw new BusinessException(ErrorCode.UserEmailNotFound, "Can not find user with that email");
            }
            user.Otp = GenerateOtp();
            user.OtpExpiry = DateTime.Now.AddMinutes(5);
            _userRepository.SaveChanges();
            await EmailService.SendEmailAsync(user.Email, "", user.Otp);
            return true;

        }
        private string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
