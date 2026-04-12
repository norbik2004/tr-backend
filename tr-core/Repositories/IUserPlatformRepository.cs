using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.Entities;

namespace tr_core.Repositories
{
    public interface IUserPlatformRepository : IRepository<UserPlatform>
    {
        Task<bool> ExistsAsync(string userId, int platformId);
        Task<List<UserPlatform>> GetUserPlatformsPerUserAsync(string userId);
        Task<UserPlatform?> GetUserPlatformPerUserByIdAsync(int userPlatformId, string userId);
    }
}
