using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Tasks.Commands;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.Task;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ControllerParameters controllerParameters) : BaseController(controllerParameters)
    {
        [HttpGet("getall")]
        //[Authorize]
        public async Task<ResultVM<IEnumerable<ProjectTaskVM>>> GetAllAsync()
        {
            var tasks = await mediator.Send(new GetAllTasksQuery());

            var result = tasks.AsQueryable().Map<ProjectTaskVM>().AsEnumerable();

            return ResultVM<IEnumerable<ProjectTaskVM>>.Sucess(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddTaskVM addTaskVM)
        {
            var result = await mediator.Send(addTaskVM.MapOne<AddTaskCommand>());
            return Ok(ResultVM<bool>.Sucess(true));

        }
    }
}
