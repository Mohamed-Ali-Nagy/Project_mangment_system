using Microsoft.AspNetCore.Mvc;

namespace Project_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost]
        public IActionResult ResetPassword()
        {
            return Ok();
        }
    }
}
