using MediatR;
using Project_management_system.Repositories;
using Project_management_system.Models;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.Services.TokenGenerator;
using Microsoft.AspNetCore.Identity;
using Project_management_system.Exceptions;
using Project_management_system.Enums;
using BCrypt.Net;
using Project_management_system.Helpers;
namespace Project_management_system.CQRS.Users.Commands
{
    public record UserLoginCommand(UserLoginDTO userLoginDTO) : IRequest<string>;
    public record UserLoginDTO(string Email,string Password);
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>

    {
        private readonly IBaseRepository<User> _userrepository;
        private readonly IMediator _mediator;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserLoginCommandHandler(IBaseRepository<User> userrepository, 
            IMediator mediator, 
            ITokenGenerator tokenGenerator,
            IPasswordHasher<User> passwordHasher

            )
        {
            _userrepository = userrepository;  
            _mediator = mediator;
            _tokenGenerator = tokenGenerator;
            _passwordHasher = passwordHasher;
        }
        
        public async Task< bool> ValidateUser( UserLoginDTO userDTO)
        {
            var logedinUser = MapperHelper.MapOne<User>(userDTO);
            var user = await _mediator.Send(new GetUserByEmailQuery(logedinUser.Email));
            var result = BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password);
            // var verificationResult = _passwordHasher.VerifyHashedPassword( user, user.Password, userDTO.Password);
            //if (user != null && verificationResult == PasswordVerificationResult.Success) {

             if (user != null && result) {
           // if (user!=null && user.Password == logedinUser.Password) { 
                return true;
            }
            return false;
            
        }
        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.userLoginDTO.Email));
            if (await ValidateUser(request.userLoginDTO))
            {
                return  _tokenGenerator.GenerateToken(user);
     
            }
            else
            {
                throw new BusinessException(ErrorCode.WrongPasswordOrEmail,"Wrong Password or Email");
            }
        }
    }
}
