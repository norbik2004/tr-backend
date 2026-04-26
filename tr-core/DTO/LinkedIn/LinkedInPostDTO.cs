using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.LinkedIn.Request;
using tr_core.DTO.UserPlatform.Response;

namespace tr_core.DTO.LinkedIn
{
    public class LinkedInPostDTO
    {
        public required UserPlatformResponseLong UserPlatform { get; set; }
        public required LinkedInPostRequest Request { get; set; }
    }
}
