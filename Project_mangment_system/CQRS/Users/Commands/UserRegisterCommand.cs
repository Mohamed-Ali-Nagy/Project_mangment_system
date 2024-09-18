using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using Project_management_system.ViewModels;

namespace Project_management_system.CQRS.Users.Commands
{
    public record UserRegisterCommand(string name,string password,string email ,string phone,string imageURL,string country):IRequest<ResultDTO<bool>>
    {
    }
    public record UserRegisterHandler : IRequestHandler<UserRegisterCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _repository;
        public UserRegisterHandler(IMediator mediator,IBaseRepository<User> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            
            if (await _mediator.Send(new GetUserByEmailQuery(request.email)) != null)
            {
                return ResultDTO<bool>.Faliure("Error happened while register this user ");
            }
            var user=request.MapOne<User>();
            user.Otp = OTPHelper.GenerateOtp();
            user.OtpExpiry=OTPHelper.SetOtpExpiry();

            _repository.Add(user);
            _repository.SaveChanges();
          // await EmailService.SendEmailAsync(user.Email, user.Name, user.Otp);

            return ResultDTO<bool>.Sucess(true, "User Registered successfully");
        }


    }
}
