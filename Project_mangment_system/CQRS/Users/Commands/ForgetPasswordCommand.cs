using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Exceptions;
using Project_management_system.Helpers;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(string email) : IRequest<ResultDTO<bool>>
    {
    }
    public record ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Models.User> _userRepository;
        public ForgetPasswordHandler(IMediator mediator, IBaseRepository<Models.User> baseRepository)
        {
            _userRepository = baseRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            if (user == null)
            {
                return ResultDTO<bool>.Faliure("Can not find user with that email");
            }
            user.Otp = OTPHelper.GenerateOtp();
            user.OtpExpiry = OTPHelper.SetOtpExpiry();
            _userRepository.SaveChanges();
            await EmailService.SendEmailAsync(user.Email, "", user.Otp);
            return  ResultDTO<bool>.Sucess(true);
            ;

        }

    }
}
