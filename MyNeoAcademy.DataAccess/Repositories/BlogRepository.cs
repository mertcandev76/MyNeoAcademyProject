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

        public async Task<List<Blog>> GetAllWithCategoryAndAuthorAsync()
        {
            return await Table
                .Include(b=>b.Category)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Blog?> GetByIdWithCategoryAndAuthorAsync(int id)
        {
            return await Table
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b=>b.BlogID==id);
        }
    }
}
