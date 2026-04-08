using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class Platform : BaseEntity, IAuditable
    {
        public string Name { get; set; } = null!;
        public ICollection<PostPlatform> PostPlatforms { get; set; } = [];
    }
}
