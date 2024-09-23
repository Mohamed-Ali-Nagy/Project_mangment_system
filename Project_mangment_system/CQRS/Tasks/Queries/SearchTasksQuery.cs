using MediatR;
using PredicateExtensions;
using Project_management_system.Helpers;
using Project_management_system.Models;

namespace Project_management_system.CQRS.Tasks.Queries
{
    public record SearchTasksQuery(string Key) : IRequest<IEnumerable<ProjectTaskDto>>;

    public class SearchTasksQueryHandler : BaseRequestHandler<ProjectTask, SearchTasksQuery, IEnumerable<ProjectTaskDto>>
    {
        public SearchTasksQueryHandler(RequestParameters<ProjectTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<IEnumerable<ProjectTaskDto>> Handle(SearchTasksQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateExtensions.PredicateExtensions.Begin<ProjectTask>(true);

            predicate = predicate.And(t => t.Title.Contains(request.Key));

            if (Enum.TryParse<Enums.TaskStatus>(request.Key, true, out var status))
            {
                predicate = predicate.Or(p => p.Status == status);
            }

            var tasks = await Task.Run(() => _repository.Get(predicate));

            return tasks.Map<ProjectTaskDto>();
        }
    }
}
