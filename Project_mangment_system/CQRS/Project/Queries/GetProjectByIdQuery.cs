using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Enums;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Project.Queries
{
	public record GetProjectByIdQuery(int ProjectId) : IRequest<Result<Models.Project>>;

	public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<Models.Project>>
	{
		private readonly IBaseRepository<Models.Project> _baseRepository;

		public GetProjectByIdQueryHandler(IBaseRepository<Models.Project> baseRepository)
        {
			_baseRepository = baseRepository;
		}
        public async Task<Result<Models.Project>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
		{
			var project = (await _baseRepository.Get_Async(p => p.ID == request.ProjectId && !p.IsDeleted)).FirstOrDefault();
			if (project == null)
			{
				return Result.Failure<Models.Project>(ProjectError.ProjectNotFound);
			}

			return Result.Success(project);
		}
	}
}
