using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class Post : BaseEntity, IAuditable
    {

        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }

        public User User { get; set; } = null!;

        public required string PromptText { get; set; }

        public required string Body { get; set; }

        public ICollection<PostPlatform> PostPlatforms { get; set; } = new List<PostPlatform>();

    }
}
