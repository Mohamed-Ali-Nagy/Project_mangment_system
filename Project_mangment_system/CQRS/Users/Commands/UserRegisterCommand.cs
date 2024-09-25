using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record UserRegisterCommand() : IRequest<ResultDTO<bool>>
    {
        public string name;
        public string password;
        public string email;
        public string phoneNumber;
        public string imageURL;
        public string country;

    }
    public record UserRegisterHandler : IRequestHandler<UserRegisterCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _repository;
        public UserRegisterHandler(IMediator mediator, IBaseRepository<User> repository)
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
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);
            var user = request.MapOne<User>();
            user.Password = hashedPassword;
            user.Otp = OTPHelper.GenerateOtp();
            user.OtpExpiry = OTPHelper.SetOtpExpiry();

            _repository.Add(user);

            await EmailService.SendEmailAsync(user.Email, user.Name, user.Otp);
            _repository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "User Registered successfully");
        }


    }
}
