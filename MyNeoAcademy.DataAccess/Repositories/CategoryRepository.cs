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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<Category>> GetAllWithBlogAsync()
        {
            return await Table
             .Include(a => a.Blogs)
             .Include(a=>a.Courses)
             .ToListAsync();
        }

        public async Task<Category?> GetByIdWithBlogAsync(int id)
        {
            return await Table
              .Include(a => a.Blogs)
              .Include(a => a.Courses)
              .FirstOrDefaultAsync(a => a.CategoryID == id);
        }
    }
}
