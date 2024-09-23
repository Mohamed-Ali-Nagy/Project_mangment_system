using AutoMapper;
using MediatR;
using Project_management_system.DTO.UserDTOs;

namespace Project_management_system.Controllers
{
    public class ControllerParameters
    {
        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }
        public IMapper Mapper { get; set; }
        public ControllerParameters(IMediator mediator, UserState userState, IMapper mapper)
        {
            Mediator = mediator;
            UserState = userState;
            Mapper = mapper;
        }
    }
}
