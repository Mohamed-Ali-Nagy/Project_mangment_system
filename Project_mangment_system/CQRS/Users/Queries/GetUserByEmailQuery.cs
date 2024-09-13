using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_management_system.DTO.UserDTOs;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email):IRequest<UserDTO>
    {
    }
    public record GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDTO>
    {
        private IBaseRepository<User> _userRepository;
        public GetUserByEmailHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user=await _userRepository.GetAll().FirstOrDefaultAsync(u=>u.Email==request.email);
            if (user==null)
            {

            }
            return user.MapOne<UserDTO>();
        }
    }
}
