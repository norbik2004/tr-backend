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
        public async Task<UserPlatformResponse> AddUserPlatform([FromBody] UserPlatformRequest request)
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

        [HttpGet("{userPlatformId:int}")]
        public async Task<UserPlatformResponseLong> GetUserPlatformsPerUser(int userPlatformId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");

            return await userPlatformService.GetUserPlatformByIdAsync(userPlatformId, userId);
        }

        [HttpDelete("{userPlatformId:int}")]
        public async Task<IActionResult> RemoveUserPlatform(int userPlatformId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");
            await userPlatformService.RemoveUserPlatform(userPlatformId, userId);
            return NoContent();
        }

        [HttpPut("{userPlatformId:int}")]
        public async Task<UserPlatformResponseLong> UpdateUserPlatform(int userPlatformId, [FromBody] UserPlatformUpdateRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");
            return await userPlatformService.UpdateUserPlatformAsync(userPlatformId, request, userId);
        }
    }
}
