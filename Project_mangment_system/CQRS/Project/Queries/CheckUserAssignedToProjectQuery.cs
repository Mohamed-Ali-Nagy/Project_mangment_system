using MediatR;
using Project_management_system.Abstractions;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Project.Queries
{
    public record CheckUserAssignedToProjectQuery(int UserId, int ProjectId) : IRequest<Result<bool>>;

    public class CheckUserAssignedToProjectQueryHandler : IRequestHandler<CheckUserAssignedToProjectQuery, Result<bool>>
    {

        private readonly IBaseRepository<Models.ProjectsUsers> _baseRepository;

        public CheckUserAssignedToProjectQueryHandler(IBaseRepository<Models.ProjectsUsers> baseRepository)
        {

            _baseRepository = baseRepository;
        }

        public async Task<Result<bool>> Handle(CheckUserAssignedToProjectQuery request, CancellationToken cancellationToken)
        {
            var userProject = await _baseRepository
                .Get_Async(up => up.ID == request.UserId && up.ID == request.ProjectId);

            return Result.Success(userProject.Any());
        }
    }
}
