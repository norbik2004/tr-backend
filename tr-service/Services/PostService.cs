using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Post.Request;
using tr_core.DTO.Post.Response;
using tr_core.Repositories;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_service.Services
{
    public class PostService(IPostRepository postRepository, IMapper mapper) : IPostService
    {
        public async Task<List<PostResponse>> GetAllPostsAsync(PostPaginatedParamsRequest request)
        {
            var posts = await postRepository.GetAll();

            return mapper.Map<List<PostResponse>>(posts);
        }

        public async Task<PostResponse> GetPostById(int postId)
        {
            var post = await postRepository.GetByIdAsync(postId.ToString());

            if (post == null)
                throw new BadRequestException("Post was not found");

            return mapper.Map<PostResponse>(post);
        }
    }
}
