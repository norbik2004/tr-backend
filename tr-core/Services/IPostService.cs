using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Post.Request;
using tr_core.DTO.Post.Response;

namespace tr_core.Services
{
    public interface IPostService
    {
        public Task<List<PostResponse>> GetAllPostsAsync(PostPaginatedParamsRequest request);
        public Task<PostResponse> GetPostById(int postId);
        public Task<PostResponse> CreatePostAsync(PostRequest request, string userId);
    }
}
