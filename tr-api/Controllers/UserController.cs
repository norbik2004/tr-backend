using Microsoft.AspNetCore.Mvc;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("login")]
        public IActionResult LogIn()
        {
            throw new NotImplementedException();
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
