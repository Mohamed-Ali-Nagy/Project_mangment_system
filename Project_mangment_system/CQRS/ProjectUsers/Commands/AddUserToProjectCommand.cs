using MediatR;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Helpers;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.ProjectUsers.Commands
{
    public record AddUserToProjectCommand(int UserID,int ProjectID,Role Role) :IRequest<ResultDTO<bool>>;
    public class AddUserToProjectCommandHandler : IRequestHandler<AddUserToProjectCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<ProjectsUsers> _baseRrepository;
        public AddUserToProjectCommandHandler(
            IMediator mediator,
            IBaseRepository<ProjectsUsers> baseRepository)
        {
            _mediator = mediator;
            _baseRrepository = baseRepository;
        }
        public  async Task<ResultDTO<bool>> Handle(AddUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var projectUser = request.MapOne<ProjectsUsers>();
             _baseRrepository.Add(projectUser);
            _baseRrepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);

        }
    }

}
