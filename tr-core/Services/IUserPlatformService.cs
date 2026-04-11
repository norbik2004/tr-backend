using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.DTO.UserPlatform.Request;
using tr_core.DTO.UserPlatform.Response;

namespace tr_core.Services
{
    public interface IUserPlatformService
    {
        Task<UserPlatformResponse> AddUserPlatformAsync(UserPlatformRequest request, string userId);
        Task<List<UserPlatformResponse>> GetUserPlatformsAsync(string userId);
    }
}
