using MediatR;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Queries
{
    public record GetTaskByIdQuery(int Id) : IRequest<ProjectTask>;

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ProjectTask>
    {
        private IBaseRepository<ProjectTask> _repository;

        public GetTaskByIdQueryHandler(IBaseRepository<ProjectTask> repository)
        {
            _repository = repository;
        }

        public async Task<ProjectTask> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetAsync(u => u.ID.Equals(request.Id));

            return task;
        }
    }
}
