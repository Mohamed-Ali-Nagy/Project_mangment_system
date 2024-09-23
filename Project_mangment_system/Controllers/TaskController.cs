using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.DTO;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.TaskVMs;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ResultVM<IEnumerable<TaskByStatusVM>>> GetAllTasksForSpecificProject(int projectID)
        {
            var tasks = await _mediator.Send(new GetAllTasksByStatusQuery(projectID));
            var tasksVM = _mapper.Map<IEnumerable< TaskByStatusVM>>(tasks.Data);
            return ResultVM<IEnumerable<TaskByStatusVM>>.Sucess(tasksVM);
           
        }



    }
}
