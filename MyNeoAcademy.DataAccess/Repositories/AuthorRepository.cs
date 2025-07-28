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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }


        public async Task<List<Author>> GetAllWithIncludesAsync()
        {
            return await Table
             .Include(a => a.Blogs)
             .ToListAsync();
        }

        public async Task<Author?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
             .Include(a => a.Blogs)
             .FirstOrDefaultAsync(a => a.AuthorID == id);
        }
    }
}
