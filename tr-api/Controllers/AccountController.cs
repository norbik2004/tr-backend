using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tr_backend.Helpers;
using tr_core.Consts;
using tr_core.DTO.User.Request;
using tr_core.DTO.User.Response;
using tr_core.Services;
using tr_service.Exceptions;
using tr_service.Mapping;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            await userService.RegisterUserAsync(request);
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<UserResponse> GetLoggedInUserInfo()
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);

            return await userService.GetLoggedInUserInfoAsync(userId);
        }
    }
}
