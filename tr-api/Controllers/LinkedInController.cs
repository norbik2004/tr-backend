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
        public IActionResult GetAuthorizationUrl([FromQuery] int platformId)
        {
            var redirect = _config.RedirectUri;
            var clientId = _config.ClientId;
            var scopes = "openid%20profile%20w_member_social%20email";
            var state = Guid.NewGuid().ToString();

            var url = $"https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirect)}&scope={scopes}&state={state}";

            return Ok(new { url });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] int platformId)
        {
            var userId = UserHelpers.GetUserIdFromClaims(User);

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

    }
}
