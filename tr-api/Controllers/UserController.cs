using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.User;
using tr_core.Entities;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(SignInManager<User> signInManager) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(
                request.Login,
                request.Password,
                isPersistent: true,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
                return Unauthorized();

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(User.Identity.Name);
        }
    }
}
