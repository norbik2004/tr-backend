using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tr_core.DTO.Post.Request
{
    public class PostPaginatedParamsRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
