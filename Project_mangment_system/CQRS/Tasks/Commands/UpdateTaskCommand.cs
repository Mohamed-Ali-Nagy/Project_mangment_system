using MediatR;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.DTO;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Commands
{
    public record UpdateTaskCommand(int ID, string Title, string? Description, Enums.TaskStatus Status):IRequest<ResultDTO<bool>>;
    public record UpdateTaskHandler : IRequestHandler<UpdateTaskCommand,ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<ProjectTask> _repository;
        public UpdateTaskHandler(IBaseRepository<ProjectTask> repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task =await _mediator.Send(new GetTaskByIdQuery(request.ID));
            if (task == null)
            {
                return ResultDTO<bool>.Faliure("invalid task ID");
            }
            task.Title = request.Title;
            task.Description=request.Description;
            task.Status= request.Status;
            _repository.Update(task);
            _repository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }

}
