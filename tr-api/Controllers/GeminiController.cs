using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.Gemini;
using tr_core.Services.Gemini;

namespace tr_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiController(IGeminiService geminiService) : ControllerBase
    {
        [HttpPost("test-gemini")]
        public async Task<GeminiResponse> TestGemini(string prompt)
        {
            return await geminiService.SendTestRequestToGemini(prompt);
        }
    }
}
