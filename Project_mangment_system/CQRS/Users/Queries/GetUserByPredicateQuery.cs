using MediatR;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using static Project_management_system.CQRS.Users.Queries.GetUserByPredicateQueryHandler;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email):IRequest<UserDetailsDTO>;
    public class UserDetailsDTO
    {
        public int ID { get; set; }
    public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public HashSet<string> Roles { get; set; }
    }
    public class GetUserByPredicateQueryHandler :IRequestHandler<GetUserByEmailQuery, UserDetailsDTO>
    {
        private readonly IBaseRepository<User> _userRepository;
        public GetUserByPredicateQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDetailsDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncWithProjectTo<UserDetailsDTO>(user => user.Email == request.email);
            return user;
        }
    }
  
}
