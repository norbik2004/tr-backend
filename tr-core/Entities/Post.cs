using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; } = null!;

        public required string PromptText { get; set; }

        public required string Body { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
