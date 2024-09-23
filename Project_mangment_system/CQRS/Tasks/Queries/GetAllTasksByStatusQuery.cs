using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Queries
{
    public record GetAllTasksByStatusQuery(int ProjectID) :IRequest<ResultDTO<IEnumerable<TaskByStatusDTO>>>;
   public record ProjectTaskDTO(string Title,string Description,DateTime CreatedOn , string UserName);
    public record TaskByStatusDTO
    {
        public TaskByStatusDTO() // Default constructor
        {
            TaskDTO = Enumerable.Empty<ProjectTaskDTO>();
        }
      public  IEnumerable<ProjectTaskDTO> TaskDTO {  get; set; }
      public string Status { get; set; }
    }
    public class GetAllTasksByStatusQueryHandler :IRequestHandler<GetAllTasksByStatusQuery,ResultDTO<IEnumerable<TaskByStatusDTO>>>
    {
        private readonly IBaseRepository<ProjectTask> _taskRepository;
        public GetAllTasksByStatusQueryHandler(IBaseRepository<ProjectTask> taskRepository)
        {
             _taskRepository = taskRepository;
        }

        public async Task<ResultDTO<IEnumerable<TaskByStatusDTO>>> Handle(GetAllTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.Get(t=>t.ProjectID==request.ProjectID).GroupBy(t => t.Status).Map<TaskByStatusDTO>().ToListAsync();
           
                return ResultDTO<IEnumerable<TaskByStatusDTO>>.Sucess(tasks.AsEnumerable());
           
           
          
        }
    }
       
}
