using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.UserPlatform.Request;
using tr_core.DTO.UserPlatform.Response;
using tr_core.Entities;
using tr_core.Repositories;
using tr_core.Services;
using tr_service.Exceptions;

namespace tr_service.Services
{
    public class UserPlatformService(IUserPlatformRepository userPlatformRepository, IPlatformRepository platformRepository,
        IMapper mapper) : IUserPlatformService
    {
        public async Task<UserPlatformResponse> AddUserPlatformAsync(UserPlatformRequest request, string userId)
        {
            var platform = await platformRepository.GetByIdAsync(request.PlatformId.ToString())
                ?? throw new BadRequestException("Platform not found");

            var exists = await userPlatformRepository.ExistsAsync(userId, request.PlatformId);

            if (exists)
                throw new BadRequestException("User platform already exists");

            var userPlatformEntity = mapper.Map<UserPlatform>(request);
            userPlatformEntity.UserId = userId;
            userPlatformEntity.PlatformId = platform.Id;
            userPlatformEntity.Platform = platform;

            await userPlatformRepository.AddAsync(userPlatformEntity);
            await userPlatformRepository.SaveChangesAsync();

            return mapper.Map<UserPlatformResponse>(userPlatformEntity);
        }

        public async Task<List<UserPlatformResponse>> GetUserPlatformsAsync(string userId)
        {
            var userPlatforms = await userPlatformRepository.GetUserPlatformsPerUserAsync(userId);

            return mapper.Map<List<UserPlatformResponse>>(userPlatforms);
        }
    }
}
