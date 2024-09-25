using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.DTO.UserDTOs;
using System.Security.Claims;

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

        protected void SetUserState()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
                userState.Id = int.Parse(userId);

            userState.Name = User?.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }
    }
}
