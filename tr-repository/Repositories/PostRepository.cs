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
        public async Task<List<Post>> GetAll()
        {
            return await dbContext.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            int identifier = Int32.Parse(id);

            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == identifier);

            return post;
        }
    }
}
