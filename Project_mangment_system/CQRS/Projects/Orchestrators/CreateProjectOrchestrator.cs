using AutoMapper;
using MediatR;
using Project_management_system.CQRS.Projects.Commands;
using Project_management_system.CQRS.ProjectUsers.Commands;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using System.Data;

namespace Project_management_system.CQRS.Projects.Orchestrators
{

    public record CreateProjectOrchestrator(string Title, string Description, DateTime CreatedOn, ProjectStatus Status, int UserID) : IRequest<ResultDTO<bool>>;
    //public 
    public class CreateProjectOrchestratorHandler : IRequestHandler<CreateProjectOrchestrator, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        public CreateProjectOrchestratorHandler(IMediator mediator)
        {
           _mediator = mediator;

        }

        public async Task<ResultDTO<bool>> Handle(CreateProjectOrchestrator request, CancellationToken cancellationToken)
        {
            var projectID = await _mediator.Send(request.MapOne<CreateProjectCommand>());
             await _mediator.Send(new AddUserToProjectCommand(request.UserID, projectID.Data, Role.Admin));
            return ResultDTO<bool>.Sucess(true,"Project Added Successfully");
        }
    }
}
