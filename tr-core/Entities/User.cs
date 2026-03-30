using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class User : IdentityUser
    {
        public bool IsSubscribed { get; set; }

        public int PromptAmount { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public UserSetting UserSetting { get; set; } = null!;
    }
}
