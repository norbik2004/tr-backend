using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.User;
using tr_core.Interfaces;

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
            try
            {
                await _userService.RegisterUserAsync(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Wystąpił błąd podczas rejestracji", error = ex.Message });
            }
        }
    }
}
