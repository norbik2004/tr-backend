using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.Post.Request;
using tr_core.DTO.Post.Response;
using tr_core.Entities;
using tr_core.Enums;
using tr_core.Repositories;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_service.Services
{
    public class PostService(IPostRepository postRepository, IMapper mapper) : IPostService
    {
        public async Task<PostResponse> CreatePostAsync(PostRequest request, string userId)
        {
            var postEntity = mapper.Map<Post>(request);

            postEntity.Status = PostStatus.Draft;
            postEntity.UserId = userId;

            /*
            var userPlatforms = await postRepository.GetUserPlatformsAsync(userId, request.UserPlatformIds);

            if (userPlatforms.Count != request.UserPlatformIds.Count)
                throw new Exception("Invalid platforms selected");

            postEntity.Publications = userPlatforms.Select(up => new PostPublication
            {
                UserPlatformId = up.Id,
                Status = "Pending"
            }).ToList();

            */

            await postRepository.AddAsync(postEntity);
            await postRepository.SaveChangesAsync();

            return new PostResponse
            {
                Id = postEntity.Id,
                Title = postEntity.Title,
                Body = postEntity.Body,
                Status = postEntity.Status,
                UserPlatformIds = postEntity.Publications
            .Select(x => x.UserPlatformId)
            .ToList()
            };
        }

        public async Task<List<PostResponse>> GetAllPostsAsync(PostPaginatedParamsRequest request)
        {
            var posts = await postRepository.GetAllAsync();

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
