using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_management_system.DTO.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
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
            GetUserState();
        }

        private void GetUserState()
        {
            if (HttpContext is not null)
            {
                string userId = HttpContext.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "";
                if (!string.IsNullOrEmpty(userId))
                    userState.Id = int.Parse(userId);
                userState.Name = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value ?? "";
            }
        }
    }
}
