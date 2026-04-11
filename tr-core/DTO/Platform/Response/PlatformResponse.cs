using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.Enums;

namespace tr_core.DTO.Platform.Response
{
    public class PlatformResponse
    {
        public int Id { get; set; }
        public PlatformType Type { get; set; }
    }
}
