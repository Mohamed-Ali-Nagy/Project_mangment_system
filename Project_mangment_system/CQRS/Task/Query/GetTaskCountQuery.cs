using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Repositories;
using Project_management_system.Specification;
using Project_management_system.Specification.TaskSpec;

namespace Project_management_system.CQRS.Task.Query
{
	public record GetTaskCountQuery(SpecParams SpecParams) : IRequest<Result<int>>;

	public class GetTaskCountQueryHandler : IRequestHandler<GetTaskCountQuery, Result<int>>
	{
		private readonly IBaseRepository<Models.Task> _baseRepository;

		public GetTaskCountQueryHandler( IBaseRepository<Models.Task> baseRepository)
        {
			_baseRepository = baseRepository;
		}
        public async Task<Result<int>> Handle(GetTaskCountQuery request, CancellationToken cancellationToken)
		{
			var taskSpec = new CountTaskWithSepc(request.SpecParams);
			var count= await _baseRepository.GetCountWithSpecAsync(taskSpec);
				return Result.Success(count);
		}
	}
}
