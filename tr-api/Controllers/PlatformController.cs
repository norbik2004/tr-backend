using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.Platform.Response;
using tr_core.Services;

namespace tr_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController(IPlatformService platformService) : ControllerBase
    {
        [HttpGet("platforms")]
        public async Task<List<PlatformResponse>> GetAllAsync()
        {
            return await platformService.GetAllAsync();
        }
    }
}
