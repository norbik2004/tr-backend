using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
    }
}
