using MediatR;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;
using Project_management_system.ViewModels;
using System.Security.Cryptography.Xml;

namespace Project_management_system.CQRS.Projects.Queries
{
    public record GetAllProjectsQuery(int pageSize, int pageNumber) : IRequest<ResultDTO<PaginatedList<ProjectListDTO>>>
    {
    }
    public record ProjectListDTO
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ProjectStatus Status { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfTasks { get; set; }

    }
    public record GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, ResultDTO<PaginatedList<ProjectListDTO>>>
    {
        private readonly IBaseRepository<Project> _repository;
        public GetAllProjectsHandler(IBaseRepository<Project> repository)
        {
            _repository = repository;
        }
        public async Task<ResultDTO<PaginatedList<ProjectListDTO>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll().Map<ProjectListDTO>();
            var projectsPaginatedList = await PaginatedList<ProjectListDTO>.CreateAsync(query, request.pageNumber, request.pageSize);
           
            return ResultDTO<PaginatedList<ProjectListDTO>>.Sucess(projectsPaginatedList);

        }
    }
}
