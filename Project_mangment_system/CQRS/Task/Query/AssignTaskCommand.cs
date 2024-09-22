using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Enums;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Task.Query
{
	public class AssignTaskCommand:IRequest<Result>
	{
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
	public class AssignedTaskCommandHandler : IRequestHandler<AssignTaskCommand, Result>
	{
		private readonly IBaseRepository<Models.Task> _baseRepository;

		public AssignedTaskCommandHandler(IBaseRepository<Models.Task> baseRepository )
        {
			_baseRepository = baseRepository;
		}
        public async Task<Result> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
		{
			var task= await _baseRepository.GetByIdAsync( request.TaskId );
			if (task == null)
			{
				return Result.Failure(TaskError.TaskNotFound);
			}

			task.UserID = request.UserId;

			_baseRepository.Update(task);
			await _baseRepository.SaveChangesAsync();

			return Result.Success();
		}
	}

}
