using AutoMapper;
using tr_core.DTO.Platform.Response;
using tr_core.DTO.Post.Request;
using tr_core.DTO.Post.Response;
using tr_core.DTO.User.Response;
using tr_core.DTO.UserPlatform.Request;
using tr_core.DTO.UserPlatform.Response;
using tr_core.Entities;

namespace tr_service.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Post, PostResponse>();
            CreateMap<PostRequest, Post>();

            CreateMap<Platform, PlatformResponse>();

            CreateMap<UserPlatform, UserPlatformResponse>();
            CreateMap<UserPlatform, UserPlatformResponseLong>();
            CreateMap<UserPlatformUpdateRequest, UserPlatform>();
            CreateMap<UserPlatformRequest, UserPlatform>();
        }
    }
}
