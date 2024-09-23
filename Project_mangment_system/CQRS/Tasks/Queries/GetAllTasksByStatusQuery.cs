using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.Repositories;
using Project_management_system.ViewModels.TaskVMs;
using System.Collections.Generic;

namespace Project_management_system.CQRS.Tasks.Queries
{
    public record GetAllTasksByStatusQuery(int projectID) :IRequest<ResultDTO<IEnumerable<TaskByStatusDTO>>>;
   public record TaskDTO(string Title,string Description,DateTime CreatedOn , string UserName);
    public record TaskByStatusDTO
    {
        public TaskByStatusDTO() // Default constructor
        {
            TaskDTO = Enumerable.Empty<TaskDTO>();
        }
      public  IEnumerable<TaskDTO> TaskDTO {  get; set; }
      public string Status { get; set; }
    }
    public class GetAllTasksByStatusQueryHandler :IRequestHandler<GetAllTasksByStatusQuery,ResultDTO<IEnumerable<TaskByStatusDTO>>>
    {
        private readonly IBaseRepository<Models.Task> _taskRepository;
        public GetAllTasksByStatusQueryHandler(IBaseRepository<Models.Task> taskRepository)
        {
             _taskRepository = taskRepository;
        }

        public async Task<ResultDTO<IEnumerable<TaskByStatusDTO>>> Handle(GetAllTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.Get(t=>t.ProjectID==request.projectID).GroupBy(t => t.Status).Map<TaskByStatusDTO>().ToListAsync();
           
                return ResultDTO<IEnumerable<TaskByStatusDTO>>.Sucess(tasks.AsEnumerable());
           
           
          
        }
    }
       
}
