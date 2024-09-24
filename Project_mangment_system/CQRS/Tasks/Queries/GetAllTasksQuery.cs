using MediatR;
using Project_management_system.Helpers;
using Project_management_system.Models;

namespace Project_management_system.CQRS.Tasks.Queries
{
    public record GetAllTasksQuery() : IRequest<IQueryable<ProjectTaskDto>>;

    public record ProjectTaskDto(string Title, string? Description, Enums.TaskStatus Status, DateTime CreatedOn, string UserName);

    public class GetAllTasksQueryHandler : BaseRequestHandler<ProjectTask, GetAllTasksQuery, IQueryable<ProjectTaskDto>>
    {
        public GetAllTasksQueryHandler(RequestParameters<ProjectTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<IQueryable<ProjectTaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await Task.Run(() => _repository.GetAll().Map<ProjectTaskDto>());

            return tasks;
        }
    }
}
