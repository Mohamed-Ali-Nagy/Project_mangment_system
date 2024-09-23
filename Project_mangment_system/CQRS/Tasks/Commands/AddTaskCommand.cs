using MediatR;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.Tasks.Commands
{
    public record AddTaskCommand() : IRequest<ResultDTO<bool>>
    {
        public int ProjectID;
        public string Title;
        public string Description;
        public Enums.TaskStatus Status;
        public DateTime CreatedOn;
    };


    public record AddTaskQuery : IRequestHandler<AddTaskCommand, ResultDTO<bool>>
    {
        private readonly IBaseRepository<ProjectTask> _repository;
        public AddTaskQuery(IBaseRepository<ProjectTask>repository)
        {
            _repository = repository;
        }
        public async Task<ResultDTO<bool>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
           var projectTask=request.MapOne<ProjectTask>();
            _repository.Add(projectTask);
           await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true);
        }
    }
}
