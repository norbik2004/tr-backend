using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.DTO.User.Request
{
    public class UserLoginRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
