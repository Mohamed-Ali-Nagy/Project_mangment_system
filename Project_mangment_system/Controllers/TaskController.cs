using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.Abstractions;
using Project_management_system.CQRS.Task.Command;
using Project_management_system.CQRS.Task.Query;
using Project_management_system.Helpers;
using Project_management_system.Specification;
using Project_management_system.ViewModels.TaskVM;

namespace Project_management_system.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TaskController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("CreateTask")]
		public async Task<Result<int>> CreateTask([FromBody] AddTaskViemModel viewModel)
		{
			var command = viewModel.Map<AddTaskCommand>();

			var response = await _mediator.Send(command);

			return response;
		}

		[HttpPost("AssignTask")]
		public async Task<Result> AssignTask([FromBody] AssignTaskViewModel viewModel)
		{
			var command = viewModel.Map<AssignTaskCommand>();

			var response = await _mediator.Send(command);
			return response;
		}


		[HttpGet("List-Tasks")]
		public async Task<Result<Pagination<TaskToReturnDto>>> GetAllTasks([FromQuery] SpecParams spec)
		{
			var result = await _mediator.Send(new GetTasksQuery(spec));
			if (!result.IsSuccess)
			{
				return Result.Failure<Pagination<TaskToReturnDto>>(result.Error);
			}

			var TasksCount = await _mediator.Send(new GetTaskCountQuery(spec));
			var paginationResult = new Pagination<TaskToReturnDto>(spec.PageSize, spec.PageIndex, TasksCount.Data, result.Data);

			return Result.Success(paginationResult);
		}
	}
}
