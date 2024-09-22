using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Enums;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Task.Query
{
	public class CheckUserAssignedToTaskQuery:IRequest<Result<bool>>
	{
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
	public class CheckUserAssignedToTaskQueryHandler : IRequestHandler<CheckUserAssignedToTaskQuery, Result<bool>>
	{
		private readonly IBaseRepository<Models.Task> _baseRepository;

		public CheckUserAssignedToTaskQueryHandler(IBaseRepository<Models.Task> baseRepository)
        {
			_baseRepository = baseRepository;
		}
        public async Task<Result<bool>> Handle(CheckUserAssignedToTaskQuery request, CancellationToken cancellationToken)
		{
			var task= await _baseRepository.GetByIdAsync(request.TaskId);
			if (task is null)
			{
				return Result.Failure<bool>(TaskError.TaskNotFound);
			}

			return Result.Success(task.UserID == request.UserId);
		}
	}

}
