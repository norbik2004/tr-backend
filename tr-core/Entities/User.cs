using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class User : IdentityUser, IAuditable
    {
        public bool IsSubscribed { get; set; }
        public int PromptAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Post> Posts { get; set; } = [];
        public UserSetting UserSetting { get; set; }
    }
}
