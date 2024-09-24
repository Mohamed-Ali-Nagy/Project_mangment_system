using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Tasks.Commands;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.Enums;
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
        public async Task<IActionResult> GetAllAsync(int pageNumber=1,int pageSize=10)
        {
            var tasks = await mediator.Send(new GetAllTasksQuery());

            var tasksVM = tasks.Map<ProjectTaskVM>();
            var paginatedList=await PaginatedList<ProjectTaskVM>.CreateAsync(tasksVM, pageNumber, pageSize);
            return Ok(ResultVM<PaginatedList<ProjectTaskVM>>.Sucess(paginatedList));
        }
        [HttpGet("GetByID")]
        public async Task<IActionResult> Get(int id)
        {
            var task=await mediator.Send(new GetTaskByIdQuery(id));
            var taskVM = task.MapOne<TaskDetailsVM>();
            return Ok(ResultVM<TaskDetailsVM>.Sucess(taskVM));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddTaskVM addTaskVM)
        {
            var result = await mediator.Send(addTaskVM.MapOne<AddTaskCommand>());
            if(result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Sucess(true));

            }
            return Ok(ResultVM<bool>.Faliure(ErrorCode.InvalidProjectID,"Can not add tase to this project"));

        }
        [HttpPost("AddUserToTask")]
        public async Task<IActionResult> AddUserToTask(AddUserTaskVM addUserTaskVM)
        {
            var result=await mediator.Send(addUserTaskVM.MapOne<AddTaskToUserCommand>());
            if(result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Sucess(true));
            }
            return Ok(ResultVM<bool>.Faliure(ErrorCode.UserNotFound, "invalid user id or task id"));
        }
        [HttpPut("Update")]
        public async Task<IActionResult>Update(UpdateTaskVM taskUpdateVM)
        {
            var result=await mediator.Send(taskUpdateVM.MapOne<UpdateTaskCommand>());
            if (!result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(ErrorCode.InvalidTaskId,"Can not update task with that id")); 
            }
            return Ok(ResultVM<bool>.Sucess(true));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteTaskCommand(id));
            if (!result.IsSuccess)
            {
                return Ok(ResultVM<bool>.Faliure(ErrorCode.InvalidTaskId, "can not find this task"));
            }
            return Ok(ResultVM<bool>.Sucess(true));
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
