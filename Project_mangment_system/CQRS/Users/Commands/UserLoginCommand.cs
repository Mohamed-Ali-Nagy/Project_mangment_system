using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
namespace Project_management_system.CQRS.Users.Commands
{
    public record UserLoginCommand(string email, string password) : IRequest<ResultDTO<string>>;
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, ResultDTO<string>>
    {
        private readonly IMediator _mediator;
        public UserLoginCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResultDTO<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            var result = BCrypt.Net.BCrypt.Verify(request.password, user.Password);
            if (user != null && result && user.IsVerified == true)
            {
                return ResultDTO<string>.Sucess(TokenHandler.GenerateToken(user));
            }
            else
            {
                return ResultDTO<string>.Faliure("Wrong Password or Email");
            }
        }
    }
}
