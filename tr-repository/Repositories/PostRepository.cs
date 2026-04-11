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
    public class PostRepository(TrDbContext dbContext) : IPostRepository
    {
        public async Task AddAsync(Post entity)
        {
            await dbContext.Posts.AddAsync(entity);
        }

        public async Task<List<Post>> GetAll()
        {
            return await dbContext.Posts.ToListAsync();
        }

        public Task<List<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            int identifier = Int32.Parse(id);

            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == identifier);

            return post;
        }

        public void Remove(Post entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
