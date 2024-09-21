using MediatR;
using Project_management_system.DTO.UserDTOs;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS
{
    public abstract class BaseRequestHandler<TModel, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TModel : BaseModel where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly IBaseRepository<TModel> _repository;
        protected readonly UserState _userState;

        protected BaseRequestHandler(RequestParameters<TModel> requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _repository = requestParameters.Repository;
            _userState = requestParameters.UserState;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
