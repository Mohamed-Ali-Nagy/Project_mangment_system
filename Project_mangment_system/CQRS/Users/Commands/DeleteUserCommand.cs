using MediatR;
using Project_management_system.CQRS.Users.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Users.Commands
{
    public record DeleteUserCommand(int id):IRequest<ResultDTO<bool>>;
    public record DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _repository;
        public DeleteUserHandler(IBaseRepository<User> repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _mediator.Send(new GetUserByIdQuery(request.id));
            if (!user.IsSuccess)
            {
                return ResultDTO<bool>.Faliure("Can not find user with this id");
            }
            _repository.Delete(user.Data);
            _repository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }


}
