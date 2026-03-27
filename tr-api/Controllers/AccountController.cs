using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.User;
using tr_core.Interfaces;
using tr_service.Exceptions;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            await _userService.RegisterUserAsync(request);
            return Ok();
        }
    }
}
