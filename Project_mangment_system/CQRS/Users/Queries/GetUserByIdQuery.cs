using MediatR;
using Project_management_system.Enums;
using Project_management_system.Models;
using Project_management_system.Repositories;
using Project_management_system.ViewModels;

namespace Project_management_system.CQRS.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<ResultVM<User>>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultVM<User>>
    {
        private IBaseRepository<User> _userRepository;

        public GetUserByIdQueryHandler(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultVM<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.ID.Equals(request.Id));

            if (user is null)
                return ResultVM<User>.Faliure(ErrorCode.UserNotFound, "User not found");

            return ResultVM<User>.Sucess(user);
        }
    }
}
