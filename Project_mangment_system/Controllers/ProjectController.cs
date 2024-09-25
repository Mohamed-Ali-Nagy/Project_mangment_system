using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Projects.Commands;
using Project_management_system.CQRS.Projects.Orchestrators;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.ProjectVMs;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize=10,int pageNumber=1)
        {
            var result =await _mediator.Send(new GetAllProjectsQuery(pageSize, pageNumber));
            if (!result.IsSuccess) 
            { 
   
                return NotFound();
            }
            return Ok(ResultVM<PaginatedList<ProjectListDTO>>.Sucess(result.Data,""));
        }

        [HttpPost("CreateProject")]
        //[Authorize]
        public async Task<ResultVM<bool>> CreateProject(CreateProjectVM projectVM)
        {
            var result = await _mediator.Send(projectVM.MapOne<CreateProjectOrchestrator>());
            return ResultVM<bool>.Sucess(result.Data, result.Message);
        }

        [HttpPut("UpdateProject")]
        public async Task<ResultVM<bool>> UpdateProject(UpdateProjectVM projectVM ,int userID)
        {
            var projectDTO = projectVM.MapOne<UpdateProjectDTO>();
            var result = await _mediator.Send(new UpdateProjectCommand(projectDTO, userID));
            return ResultVM<bool>.Sucess(result.Data, result.Message);
        }
        [HttpDelete("DeleteProject")]
        public async Task<ResultVM<bool>> DeleteProject(int userID,int projectID)
        {
        var result = await _mediator.Send(new DeleteProjectCommand(userID,projectID));
         return ResultVM<bool>.Sucess(result.Data, result.Message);
        }

}
}
