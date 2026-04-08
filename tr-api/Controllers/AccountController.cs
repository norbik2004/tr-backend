using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tr_core.DTO.User.Request;
using tr_core.DTO.User.Response;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            await userService.RegisterUserAsync(request);
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<UserResponse> GetLoggedInUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");

            return await userService.GetLoggedInUserInfoAsync(userId);

        }
    }
}
