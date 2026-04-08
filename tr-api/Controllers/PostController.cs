using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tr_core.DTO.Post.Request;
using tr_core.DTO.Post.Response;
using tr_core.DTO.User.Response;
using tr_core.Services;
using tr_service.Mapping;
using tr_service.Services;

namespace tr_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController(IPostService postService, IMapper mapper) : ControllerBase
    {

        [HttpGet("posts")]
        public async Task<PaginatedList<PostResponse>> GetAllPosts([FromQuery] PostPaginatedParamsRequest request)
        {
            var posts = await postService.GetAllPostsAsync(request);

            return await PaginatedList<PostResponse>.CreateAsync(posts.AsQueryable(), mapper, request.PageIndex, request.PageSize);
        }

        [HttpGet("posts/{id:int}")]
        public async Task<PostResponse> GetPostById(int id)
        {
            var post = await postService.GetPostById(id);
            return post;
        }
    }
}
