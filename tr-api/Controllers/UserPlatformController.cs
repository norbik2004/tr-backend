using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using tr_core.DTO.UserPlatform.Request;
using tr_core.DTO.UserPlatform.Response;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserPlatformController(IUserPlatformService userPlatformService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<UserPlatformResponse> AddUserPlatform( [FromBody] UserPlatformRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");

            return await userPlatformService.AddUserPlatformAsync(request, userId);
        }

        [HttpGet("user-platforms")]
        public async Task<List<UserPlatformResponse>> GetUserPlatformsPerUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");

            return await userPlatformService.GetUserPlatformsAsync(userId);
        }
    }
}
