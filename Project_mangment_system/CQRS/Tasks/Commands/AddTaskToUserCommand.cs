using MediatR;
using Project_management_system.DTO;

namespace Project_management_system.CQRS.Tasks.Commands
{
    public record AddTaskToUserCommand(int taskID,int userID):IRequest<ResultDTO<bool>>
    {
    }


}
