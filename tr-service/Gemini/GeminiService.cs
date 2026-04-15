using dotenv.net;
using Google.GenAI;
using Google.GenAI.Types;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Gemini;
using tr_core.Services.Gemini;
using tr_service.Exceptions;

namespace tr_service.Gemini
{
    public class GeminiService(ILogger<IGeminiService> logger) : IGeminiService
    {
        public async Task<GeminiResponse> SendTestRequestToGemini(string prompt)
        {
            var envVars = DotEnv.Read();


            var geminiClient = new Client(apiKey: envVars["GEMINI_API_KEY"]);

            logger.LogInformation("Gemini client has been created");

            var response = await geminiClient.Models.GenerateContentAsync(
              model: "gemini-3-flash-preview", contents: prompt
            );

            logger.LogInformation("Gemini client has generated a response");

            var geminiResponse = new GeminiResponse 
            { 
                Response = response.Candidates[0].Content.Parts[0].Text  ?? ""
            };

            return geminiResponse;
        }
    }
}
