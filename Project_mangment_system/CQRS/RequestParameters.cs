using MediatR;
using Project_management_system.DTO.UserDTOs;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS
{
    public class RequestParameters<T>
    {
        public IMediator Mediator { get; set; }
        public IBaseRepository<T> Repository { get; set; }
        public UserState UserState { get; set; }

        public RequestParameters(IMediator mediator, IBaseRepository<T> repository, UserState userState)
        {
            Mediator = mediator;
            Repository = repository;
            UserState = userState;
        }
    }
}
