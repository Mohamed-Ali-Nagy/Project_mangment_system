using MediatR;
using Project_management_system.CQRS.ProjectUsers.Queries;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Projects.Commands
{
        public record DeleteProjectCommand(int userID,int projectID) : IRequest<ResultDTO<bool>>;
        public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultDTO<bool>>
        {   private readonly IMediator _mediator;
            private readonly IBaseRepository<Project> _projectRepository;
        public DeleteProjectCommandHandler( IBaseRepository<Project> projectRepository, IMediator mediator)
            {
                _projectRepository = projectRepository;
            _mediator = mediator;
            }
            public async Task<ResultDTO<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
            var result = await _mediator.Send(new GetProjectForSpecificUserQuery(request.userID, request.projectID));
            if (!result.IsSuccess) return ResultDTO<bool>.Faliure(result.Message);
            var project = await _projectRepository.GetByID(request.projectID);
            if (project is null) return ResultDTO<bool>.Faliure("invalid project id");
            _projectRepository.Delete(request.projectID);
            _projectRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
            }
        }
    
}
