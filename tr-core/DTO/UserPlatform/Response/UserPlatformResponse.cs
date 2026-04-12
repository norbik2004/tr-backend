using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Platform.Response;

namespace tr_core.DTO.UserPlatform.Response
{
    public class UserPlatformResponse
    {
        public int Id { get; set; }
        public required string AccountUsername { get; set; }
        public PlatformResponse Platform { get; set; } = new PlatformResponse();
    }
}
