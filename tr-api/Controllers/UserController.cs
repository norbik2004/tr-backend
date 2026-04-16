using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.Consts;
using tr_core.DTO.User.Request;
using tr_core.DTO.User.Response;
using tr_core.DTO.UserPlatform.Response;
using tr_core.Services;
using tr_service.Mapping;
using tr_service.Services;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("users")]
        [ProducesResponseType(typeof(PaginatedList<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<PaginatedList<UserResponse>> GetUsers([FromQuery] UserPaginatedParamsRequest request)
        {
            var users = await userService.GetAllUsers(request);

            return await PaginatedList<UserResponse>.CreateAsync(users.AsQueryable(), mapper, request.PageIndex, request.PageSize);
        }

        [Authorize]
        [HttpGet("settings")]
        [ProducesResponseType(typeof(UserSettingsResponse), StatusCodes.Status200OK)]
        public async Task<UserSettingsResponse> GetMySettings()
        {
            var userId = Helpers.UserHelpers.GetUserIdFromClaims(User);
            return await userService.GetSettingsAsync(userId);
        }

        [Authorize]
        [HttpPut("settings")]
        [ProducesResponseType(typeof(UserSettingsResponse), StatusCodes.Status200OK)]
        public async Task<UserSettingsResponse> UpdateMySettings([FromBody] UserSettingsRequest request)
        {
            var userId = Helpers.UserHelpers.GetUserIdFromClaims(User);
            return await userService.UpdateSettingsAsync(userId, request);
        }

    }
}
