using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Gemini;

namespace tr_core.Services.Gemini
{
    public interface IGeminiService
    {
        public Task<GeminiResponse> SendTestRequestToGemini(string prompt);
    }
}
