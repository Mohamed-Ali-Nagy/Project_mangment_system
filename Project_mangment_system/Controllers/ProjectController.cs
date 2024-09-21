using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Projects.Queries;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.ProjectVMs;
using Project_management_system.CQRS.Projects.Commands;
using Project_management_system.Helpers;
using Project_management_system.Models;
using AutoMapper;
using Project_management_system.CQRS.Users.Commands;
using Project_management_system.CQRS.Projects.Orchestrators;

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
        [HttpPost("CreateProject")]
        public async Task<ResultVM<bool>> CreateProject(CreateProjectVM projectVM)
        {
                return NotFound();
            }
            return Ok(ResultVM<PaginatedList<ProjectListDTO>>.Sucess(result.Data,""));
            var result = await _mediator.Send(projectVM.MapOne<CreateProjectOrchestrator>());
            return ResultVM<bool>.Sucess(result.Data,result.Message);
        }


    }
}
