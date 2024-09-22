using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.CQRS.Project.Queries;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Task.Command
{
	public record AddTaskCommand(string Title, int ProjectId, int? AssignedToUserId) : IRequest<Result<int>>;
	public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Result<int>>
	{
		private readonly Repositories.IBaseRepository<Models.Task> _baseRepository;
		private readonly IMediator _mediator;

		public AddTaskCommandHandler(IBaseRepository<Models.Task> baseRepository,IMediator mediator)
        {
			_baseRepository = baseRepository;
			_mediator = mediator;
		}
        public async Task<Result<int>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
		{
			var projectResult = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId));
			if (!projectResult.IsSuccess)
			{
				return Result.Failure<int>(ProjectError.ProjectNotFound);
			}
			var isUserAssignedToProject = (await _mediator.Send(new CheckUserAssignedToProjectQuery((int)request.AssignedToUserId!, request.ProjectId))).Data;
			if (!isUserAssignedToProject)
			{
				return Result.Failure<int>(ProjectError.UserIsNotAssignedToThisProject);
			}
			var task= request.Map<Models.Task>();
			await _baseRepository.AddAsync(task);
			await _baseRepository.SaveChangesAsync();
			return Result.Success(task.ID);
		}

	}

}
