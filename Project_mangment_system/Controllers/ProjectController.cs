using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Projects.Orchestrators;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.Helpers;
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
        public async Task<ResultVM<bool>> CreateProject(CreateProjectVM projectVM)
        {
            var result = await _mediator.Send(projectVM.MapOne<CreateProjectOrchestrator>());
            return ResultVM<bool>.Sucess(result.Data, result.Message);
        }

    }
}
