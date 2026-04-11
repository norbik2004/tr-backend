using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tr_core.Entities;
using tr_core.Repositories;

namespace tr_repository.Repositories
{
    public class UserPlatformRepository(TrDbContext dbContext) : IUserPlatformRepository
    {
        public async Task AddAsync(UserPlatform entity)
        {
            await dbContext.UserPlatforms.AddAsync(entity);
        }

        public async Task<bool> ExistsAsync(string userId, int platformId)
        {
            return await dbContext.UserPlatforms
                .AnyAsync(up => up.UserId == userId && up.PlatformId == platformId);
        }

        public async Task<List<UserPlatform>> GetAllAsync()
        {
            return await dbContext.UserPlatforms.ToListAsync();
        }

        public async Task<UserPlatform?> GetByIdAsync(string id)
        {
            return await dbContext.UserPlatforms.FirstOrDefaultAsync(up => up.Id == Int32.Parse(id));
        }

        public async Task<List<UserPlatform>> GetUserPlatformsPerUserAsync(string userId)
        {
            return await dbContext.UserPlatforms
                .Include(p => p.Platform)
                .Where(up => up.UserId == userId).ToListAsync();
        }

        public void Remove(UserPlatform entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update(UserPlatform entity)
        {
            throw new NotImplementedException();
        }
    }
}
