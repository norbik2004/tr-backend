using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.Enums;

namespace tr_core.DTO.Gemini.Request
{
    public class GeminiRequest
    {
        public required string Prompt { get; set; }
        public required GeminiModelType Model {  get; set; }
    }
}
