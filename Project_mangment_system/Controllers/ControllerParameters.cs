using MediatR;
using Project_management_system.DTO.UserDTOs;

namespace Project_management_system.Controllers
{
    public class ControllerParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }

        public ControllerParameters(IMediator mediator, UserState userState)
        {
            Mediator = mediator;
            UserState = userState;
        }
    }
}
