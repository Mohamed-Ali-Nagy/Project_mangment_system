using AutoMapper;
using MediatR;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using System.Data;

namespace Project_management_system.CQRS.Projects.Commands
{
    
    public record CreateProjectCommand(string Title, string Description, DateTime CreatedOn, ProjectStatus Status,int UserID) : IRequest<ResultDTO<int>>;
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ResultDTO<int>>
    {
        private readonly IBaseRepository<Project> _projectRepository;
   
        public CreateProjectCommandHandler(IBaseRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
            
        }
        public async Task<ResultDTO<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project =   request.MapOne<Project>();
            _projectRepository.Add(project);
            _projectRepository.SaveChanges();
            return ResultDTO<int>.Sucess(project.ID);
        }
    }
}
