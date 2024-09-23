using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record VerifyOTPCommand(string email, string otpCode) : IRequest<ResultDTO<bool>>
    {
    }
    public record VerifyOTPHandler : IRequestHandler<VerifyOTPCommand, ResultDTO<bool>>
    {
        private IMediator _mediator;
        private IBaseRepository<Models.User> _userRepository;

        public VerifyOTPHandler(IMediator mediator, IBaseRepository<Models.User> userRepository)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            if (user == null)
            {
                return ResultDTO<bool>.Faliure("Can not find user with that email");
            }
            if (user.OtpExpiry == null || user.Otp != request.otpCode || user.OtpExpiry < DateTime.Now)
            {

                return ResultDTO<bool>.Faliure("Invalid OTP");

            }
            user.IsVerified = true;
            user.Otp = null;
            user.OtpExpiry = null;
            _userRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }
}
