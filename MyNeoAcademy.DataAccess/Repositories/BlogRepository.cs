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

        public async Task<List<Blog>> GetAllWithBlogCategoryAsync()
        {
            return await Table
                .Include(b=>b.BlogCategory)
                .ToListAsync();
        }

        public async Task<Blog?> GetByIdWithBlogCategoryAsync(int id)
        {
            return await Table
                .Include(b => b.BlogCategory)
                .FirstOrDefaultAsync(b=>b.BlogID==id);
        }
    }
}
