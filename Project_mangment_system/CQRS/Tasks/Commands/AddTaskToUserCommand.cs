using MediatR;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.DTO;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Commands
{
    public record AddTaskToUserCommand(int taskID,int userID):IRequest<ResultDTO<bool>>
    {
    }
    public record AddTaskToUserHandler : IRequestHandler<AddTaskToUserCommand, ResultDTO<bool>>
    {
        private readonly IBaseRepository<ProjectTask> _repository;
        private readonly IMediator _mediator;
        public AddTaskToUserHandler(IBaseRepository<ProjectTask>repository,IMediator mediator)
        {
            _repository= repository;
            _mediator= mediator;
        }
        public async Task<ResultDTO<bool>> Handle(AddTaskToUserCommand request, CancellationToken cancellationToken)
        {
            var task =await _mediator.Send(new GetTaskByIdQuery(request.taskID));
            if (task==null)
            {
                return ResultDTO<bool>.Faliure("can not find task with that id");
            }
            task.UserID = request.userID;
            _repository.SaveChanges();
            return ResultDTO<bool>.Sucess(true, "user added successfully");
        }
    }

}
