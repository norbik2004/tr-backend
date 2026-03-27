using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.User;
using tr_core.Entities;
using tr_service.Exceptions;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(SignInManager<User> signInManager) : ControllerBase
    {
        private readonly SignInManager<User> _signInManager = signInManager;

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginRequest request)
        {
            if(User.Identities.Any(i => i.IsAuthenticated))
                throw new BadRequestException("User is arelady logged in");

            var result = await _signInManager.PasswordSignInAsync(
                request.UserName,
                request.Password,
                isPersistent: true,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
                throw new UnauthorizedException("Wrong password or username");

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
