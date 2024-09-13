using MediatR;
using Project_management_system.CQRS.OTPs.Queries;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record VerifyOTPCommand(string email,string otpCode):IRequest<bool>
    {
    }
    public record VerifyOTPHandler : IRequestHandler<VerifyOTPCommand, bool>
    {
        private IMediator _mediator;
        
        public VerifyOTPHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user =await _mediator.Send(new GetUserByEmailQuery(request.email));
            if (user == null)
            {

            }
            var otp = await _mediator.Send(new GetOtpByUserIDQuery(user.ID));
            if(otp == null || otp.OtpExpiry > DateTime.Now)
            {

            }
            return true;
        }
    }
}
