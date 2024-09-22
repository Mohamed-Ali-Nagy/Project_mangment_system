using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Helpers;
using Project_management_system.Repositories;
using Project_management_system.Specification;
using Project_management_system.Specification.TaskSpec;

namespace Project_management_system.CQRS.Task.Query
{
	public record GetTasksQuery(SpecParams SpecParams) : IRequest<Result<IEnumerable<TaskToReturnDto>>>;
	public class TaskToReturnDto
	{
		public string Title { get; set; }
		public string Status { get; set; }
		public string User { get; set; }
		public string Project { get; set; }
		public DateTime DateCreated { get; set; }
	}
	public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, Result<IEnumerable<TaskToReturnDto>>>
	{
		private readonly IBaseRepository<Models.Task> _baseRepository;

		public GetTasksQueryHandler(IBaseRepository<Models.Task> baseRepository)
        {
			_baseRepository = baseRepository;
		}
        public async Task<Result<IEnumerable<TaskToReturnDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
		{
			var spec = new TaskSpec(request.SpecParams);

			var tasks = await _baseRepository.GetAllWithSpecAsync(spec);

			var mappedProject = tasks.Map<IEnumerable<TaskToReturnDto>>();

			return Result.Success(mappedProject);
		}
	}
}
