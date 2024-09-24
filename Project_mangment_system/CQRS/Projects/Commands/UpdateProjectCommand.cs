using MediatR;
using Project_management_system.CQRS.ProjectUsers.Queries;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Exceptions;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Projects.Commands
{
    public record UpdateProjectCommand(UpdateProjectDTO projectDTO,int UserID) :IRequest<ResultDTO<bool>>;
    public class UpdateProjectDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
    }
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<Project> _projectRepository;
        public UpdateProjectCommandHandler(IMediator mediator, IBaseRepository<Project> projectRepository)
        {
            _mediator = mediator;
            _projectRepository = projectRepository;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProjectForSpecificUserQuery( request.UserID,request.projectDTO.ID));
            if (!result.IsSuccess) return ResultDTO<bool>.Faliure(result.Message);
            var project = await _projectRepository.GetByID(request.projectDTO.ID);
            if (project is null) return ResultDTO<bool>.Faliure("invalid project id");
            var updatedProject = request.projectDTO.MapOne<Project>();
            project.Title = updatedProject.Title;
            project.Description = updatedProject.Description;
            project.Status = updatedProject.Status;
            _projectRepository.Update(project);
            _projectRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }

}
