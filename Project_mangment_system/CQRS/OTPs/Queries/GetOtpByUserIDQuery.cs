using MediatR;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.OTPs.Queries
{
    public record GetOtpByUserIDQuery(int userID):IRequest<OTP>
    {
    }
    public record GetOtpByUserIDHandler : IRequestHandler<GetOtpByUserIDQuery, OTP>
    {
        private IBaseRepository<OTP> _repository;
        public GetOtpByUserIDHandler(IBaseRepository<OTP> repository)
        {
            _repository = repository;
        }
        public Task<OTP> Handle(GetOtpByUserIDQuery request, CancellationToken cancellationToken)
        {
            var otp=_repository.GetAll().Where(o=>o.UserID==request.userID).OrderBy(o=>o.OtpExpiry).FirstOrDefaultAsync();
             if(otp==null)
            {

            }
            return otp;
        }
    }
}
