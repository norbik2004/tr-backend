using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.DTO.Post.Request
{
    public class PostRequest
    {
        [MaxLength(200)]
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? PromptText { get; set; }
        public List<int> UserPlatformIds { get; set; } = [];
    }
}
