using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.User;
using tr_core.Entities;
using tr_core.Interfaces;

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
            try
            {
                if(User.Identities.Any(i => i.IsAuthenticated))
                    return BadRequest(new { message = "Użytkownik jest już zalogowany" });

                var result = await _signInManager.PasswordSignInAsync(
                    request.UserName,
                    request.Password,
                    isPersistent: true,
                    lockoutOnFailure: false
                );

                if (!result.Succeeded)
                    return Unauthorized();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Wystąpił błąd podczas logowania", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Wystąpił błąd podczas wylogowywania", error = ex.Message });
            }
        }
    }
}
