using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.DTO.UserDTOs;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator mediator;
        protected readonly UserState userState;

        public BaseController(ControllerParameters controllerParameters)
        {
            mediator = controllerParameters.Mediator;
            userState = controllerParameters.UserState;
        }
    }
}
