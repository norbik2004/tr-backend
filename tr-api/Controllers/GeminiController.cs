using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.Enums;
using tr_core.DTO.Gemini;
using tr_core.Services.Gemini;
using tr_core.DTO.Gemini.Request;

namespace tr_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiController(IGeminiService geminiService) : ControllerBase
    {
        [HttpPost("ask-gemini")]
        public async Task<GeminiResponse> TestGemini( [FromForm] GeminiRequest request)
        {
            return await geminiService.SendRequestToGemini(request);
        }
    }
}
