using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.Services;
using tr_backend.Helpers;
using System.Threading.Tasks;
using tr_core.DTO.UserPlatform.Request;
using tr_service.LinkedIn;
using tr_core.DTO.LinkedIn;
using tr_core.DTO.LinkedIn.Request;

namespace tr_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LinkedInController(ILinkedInService _linkedInService, IUserPlatformService _userPlatformService, LinkedInConfig _lconfig, IConfiguration _config) : ControllerBase
    {

        [HttpGet("authorize")]
        public IActionResult GetAuthorizationUrl()
        {
            var redirect = _lconfig.RedirectUri;
            var clientId = _lconfig.ClientId;
            var scopes = "openid%20profile%20w_member_social%20email";
            var state = $"{Guid.NewGuid()}|1";

            var url = $"{_config["LinkedIn:BaseUrl"]}authorization?response_type=code&client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirect)}&scope={scopes}&state={Uri.EscapeDataString(state)}";

            return Ok(new { url });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);

            if (string.IsNullOrEmpty(state) || !state.Contains('|'))
                return BadRequest("Missing or invalid state parameter");

            var parts = state.Split('|');
            if (!int.TryParse(parts.Last(), out var platformId))
                return BadRequest("Invalid platform id in state");

            var accessToken = await _linkedInService.ExchangeCodeForAccessToken(code, _lconfig.RedirectUri);
            var personId = await _linkedInService.GetPersonId(accessToken);

            var request = new UserPlatformRequest
            {
                PlatformId = platformId,
                AccessToken = accessToken,
                ExternalAccountId = personId,
                AccountUsername = personId,
                AccountComment = ""
            };

            var result = await _userPlatformService.AddUserPlatformAsync(request, userId);

            return Ok(result);
        }

        [HttpPost("post")]
        public async Task<IActionResult> CreatePost([FromBody] LinkedInPostRequest request)
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);
            var userPlatform = await _userPlatformService.GetUserPlatformByIdAsync(request.UserPlatformId, userId);

            if (string.IsNullOrEmpty(userPlatform.AccessToken) || string.IsNullOrEmpty(userPlatform.ExternalAccountId))
                return BadRequest("Missing access token or external account id for the selected platform");

            var linkedInPostDTO = new LinkedInPostDTO
            {
                UserPlatform = userPlatform,
                Request = request
            };

            await _linkedInService.PostTextAsync(linkedInPostDTO);

            return Ok();
        }

    }
}
