using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.ProjectUsers.Commands;
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
        [HttpPost("AddUserToTask")]
        public async Task<IActionResult> AddUserToTask(AddUserTaskVM addUserTaskVM)
        {
            var result=await mediator.Send(addUserTaskVM.MapOne<AddTaskToUserCommand>());
            if(result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Sucess(true));
            }
            return Ok(ResultVM<bool>.Faliure(Enums.ErrorCode.UserNotFound, "invalid user id or task id"));
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<ResultVM<IEnumerable<ProjectTaskVM>>> SearchAsync([FromQuery] string text)
        {
            var tasks = await mediator.Send(new SearchTasksQuery(text));

            var result = tasks.AsQueryable().Map<ProjectTaskVM>().AsEnumerable();

            return ResultVM<IEnumerable<ProjectTaskVM>>.Sucess(result);
        }
    }
}
