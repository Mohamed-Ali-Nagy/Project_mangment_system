﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.CQRS.Tasks.Queries;
using Project_management_system.Helpers;
using Project_management_system.ViewModels;
using Project_management_system.ViewModels.TaskVMs;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController(ControllerParameters controllerParameters) : BaseController(controllerParameters)
    {
        
        [HttpGet("getall")]
       // [Authorize]
        public async Task<ResultVM<IEnumerable<ProjectTaskVM>>> GetAllAsync()
        {
            var tasks = await mediator.Send(new GetAllTasksQuery());

            var result = tasks.AsQueryable().Map<ProjectTaskVM>().AsEnumerable();

            return ResultVM<IEnumerable<ProjectTaskVM>>.Sucess(result);
        }

        [HttpGet("search")]
        //[Authorize]
        public async Task<ResultVM<IEnumerable<ProjectTaskVM>>> SearchAsync([FromQuery] string text)
        {
            var tasks = await mediator.Send(new SearchTasksQuery(text));

            var result = tasks.AsQueryable().Map<ProjectTaskVM>().AsEnumerable();

            return ResultVM<IEnumerable<ProjectTaskVM>>.Sucess(result);
        }
        [HttpGet]
        public async Task<ResultVM<IEnumerable<TaskByStatusVM>>> GetAllTasksForSpecificProject(int projectID)
        {
            var tasks = await mediator.Send(new GetAllTasksByStatusQuery(projectID));
            var result = tasks.Data.AsQueryable().Map<TaskByStatusVM>().AsEnumerable();
            //var tasksVM = Mapper.Map<IEnumerable<TaskByStatusVM>>(tasks.Data);
            return ResultVM<IEnumerable<TaskByStatusVM>>.Sucess(result);

        }
    }
}
