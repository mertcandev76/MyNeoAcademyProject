using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<Blog>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .Include(b => b.BlogTags)
                    .ThenInclude(bt => bt.Tag)
                .ToListAsync();
        }

        public async Task<Blog?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                 .Include(b => b.Author)
                 .Include(b => b.Category)
                 .Include(b => b.Comments)
                 .Include(b => b.BlogTags)
                     .ThenInclude(bt => bt.Tag)
                 .FirstOrDefaultAsync(b => b.BlogID == id);
        }
    }
}
