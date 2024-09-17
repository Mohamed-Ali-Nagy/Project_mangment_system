using MediatR;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetUserByPredicateQuery(string email):IRequest<UserDetailsDTO>;
    public class UserDetailsDTO
    {
        public int ID { get; set; }
    public string Name { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public HashSet<string> Roles { get; set; }
    }
    public class GetUserByPredicateQueryHandler :IRequestHandler<GetUserByPredicateQuery, UserDetailsDTO>
    {
        private readonly IBaseRepository<User> _userRepository;
        public GetUserByPredicateQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDetailsDTO> Handle(GetUserByPredicateQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsyncWithProjectTo<UserDetailsDTO>(user => user.Email == request.email);
            return user;
        }
    }
  
}
