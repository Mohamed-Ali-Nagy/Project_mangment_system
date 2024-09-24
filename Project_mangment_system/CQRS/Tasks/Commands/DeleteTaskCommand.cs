using MediatR;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.DTO;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Commands
{
    public record DeleteTaskCommand(int id):IRequest<ResultDTO<bool>>;
    public record DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<ProjectTask> _repository;
        public DeleteTaskHandler(IMediator mediator,IBaseRepository<ProjectTask> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task=await _mediator.Send(new GetTaskByIdQuery(request.id));
            if (task == null)
            {
                return ResultDTO<bool>.Faliure("Can not find task with that id");
            }
            _repository.Delete(task);
            _repository.SaveChanges();

            return ResultDTO<bool>.Sucess(true);

        }
    }

}
