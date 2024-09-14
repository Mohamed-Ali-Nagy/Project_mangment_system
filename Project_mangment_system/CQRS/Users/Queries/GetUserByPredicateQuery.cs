using MediatR;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email):IRequest<User>;
    public class GetUserByPredicateQueryHandler :IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IBaseRepository<User> _userRepository;
        public GetUserByPredicateQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.Email == request.email);
            return user;
        }
    }
  
}
