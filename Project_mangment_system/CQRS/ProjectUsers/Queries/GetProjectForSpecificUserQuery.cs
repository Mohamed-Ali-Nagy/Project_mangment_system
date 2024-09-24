using MediatR;
using Microsoft.EntityFrameworkCore;
using Project_management_system.DTO;
using Project_management_system.Enums;
using Project_management_system.Models;
using Project_management_system.Repositories;

namespace Project_management_system.CQRS.ProjectUsers.Queries
{
    public record GetProjectForSpecificUserQuery(int userID, int projectID) : IRequest<ResultDTO<bool>>;
    public class GetProjectForSpecificUserQueryHandler : IRequestHandler<GetProjectForSpecificUserQuery, ResultDTO<bool>>
    {
        private readonly IBaseRepository<ProjectsUsers> _projectUserRepository;
        public GetProjectForSpecificUserQueryHandler(IBaseRepository<ProjectsUsers> projectUserRepository)
        {
            _projectUserRepository = projectUserRepository;
        }

        public async Task<ResultDTO<bool>> Handle(GetProjectForSpecificUserQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectUserRepository.Get(pu => pu.ProjectID == request.projectID 
                                                                      && pu.UserID == request.userID
                                                                      &&pu.Role==Role.Admin).FirstOrDefaultAsync();
            if (project != null) return ResultDTO<bool>.Sucess(true);
            else return ResultDTO<bool>.Faliure("invalid user Role");
        }
    }


}
