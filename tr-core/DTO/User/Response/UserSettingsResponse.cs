using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.DTO.User.Response
{
    public class UserSettingsResponse
    {
        public string UserId { get; set; } = string.Empty;
        public bool IsDarkMode { get; set; }
        public bool ReceiveNotifications { get; set; }
    }
}
