using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Enums;
using Project_management_system.Exceptions;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
namespace Project_management_system.CQRS.Users.Commands
{
    public record UserLoginCommand(UserLoginDTO userLoginDTO) : IRequest<string>;
    public record UserLoginDTO(string Email,string Password);
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>

    {
        private readonly IBaseRepository<User> _userrepository;
        private readonly IMediator _mediator;
        public UserLoginCommandHandler(IBaseRepository<User> userrepository, 
            IMediator mediator

            )
        {
            _userrepository = userrepository;  
            _mediator = mediator;
        }
        
        public async Task< bool> ValidateUser( UserLoginDTO userDTO)
        {
            var logedinUser = MapperHelper.MapOne<User>(userDTO);
            var user = await _mediator.Send(new GetUserByPredicateQuery(logedinUser.Email));
            //var result = BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password);

             //if (user != null && result) {
            if (user!=null && user.Password == logedinUser.Password) { 
                return true;
            }
            return false;
            
        }
        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByPredicateQuery(request.userLoginDTO.Email));
            if (await ValidateUser(request.userLoginDTO))
            {
                return  TokenHandler.GenerateToken(user);
     
            }
            else
            {
                throw new BusinessException(ErrorCode.WrongPasswordOrEmail,"Wrong Password or Email");
            }
        }
    }
}
