using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    [PrimaryKey(nameof(PostId), nameof(PlatformId))]
    public class PostPlatform
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; } = null!;

        [ForeignKey("Platform")]
        public int PlatformId { get; set; }

        public Platform Platform { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
