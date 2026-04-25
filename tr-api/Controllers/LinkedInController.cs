using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.Services;
using tr_backend.Helpers;
using System.Threading.Tasks;
using tr_core.DTO.UserPlatform.Request;
using tr_service.LinkedIn;

namespace tr_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LinkedInController : ControllerBase
    {
        private readonly ILinkedInService _linkedInService;
        private readonly IUserPlatformService _userPlatformService;
        private readonly LinkedInConfig _config;

        public LinkedInController(ILinkedInService linkedInService, IUserPlatformService userPlatformService, LinkedInConfig config)
        {
            _linkedInService = linkedInService;
            _userPlatformService = userPlatformService;
            _config = config;
        }

        [HttpGet("authorize")]
        public IActionResult GetAuthorizationUrl(/*[FromQuery] int platformId*/)
        {
            var redirect = _config.RedirectUri;
            var clientId = _config.ClientId;
            var scopes = "openid%20profile%20w_member_social%20email";
            //var state = $"{Guid.NewGuid()}|{platformId}";
            var state = $"{Guid.NewGuid()}|1";

            var url = $"https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirect)}&scope={scopes}&state={Uri.EscapeDataString(state)}";

            return Ok(new { url });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);

            if (string.IsNullOrEmpty(state) || !state.Contains('|'))
                return BadRequest("Missing or invalid state parameter");

            var parts = state.Split('|');
            if (!int.TryParse(parts[^1], out var platformId))
                return BadRequest("Invalid platform id in state");

            var accessToken = await _linkedInService.ExchangeCodeForAccessToken(code, _config.RedirectUri);
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
        public async Task<IActionResult> CreatePost([FromBody] tr_core.DTO.LinkedIn.Request.LinkedInPostRequest request)
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);
            var userPlatform = await _userPlatformService.GetUserPlatformByIdAsync(request.UserPlatformId, userId);

            if (string.IsNullOrEmpty(userPlatform.AccessToken) || string.IsNullOrEmpty(userPlatform.ExternalAccountId))
                return BadRequest("Missing access token or external account id for the selected platform");

            var authorUrn = $"urn:li:person:{userPlatform.ExternalAccountId}";

            await _linkedInService.PostTextAsync(userPlatform.AccessToken!, authorUrn, request.Text);

            return Ok();
        }

    }
}
