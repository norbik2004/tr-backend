using Microsoft.AspNetCore.Mvc;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("login")]
        public IActionResult LogIn()
        {
            
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {

        }
    }
}
