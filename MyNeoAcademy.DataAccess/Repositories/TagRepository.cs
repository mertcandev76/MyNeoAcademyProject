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
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<Tag>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(t=>t.BlogTags)
                .ToListAsync();
        }

        public async Task<Tag?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
              .Include(t => t.BlogTags)
              .FirstOrDefaultAsync(t=>t.TagID==id);
        }
    }
}
