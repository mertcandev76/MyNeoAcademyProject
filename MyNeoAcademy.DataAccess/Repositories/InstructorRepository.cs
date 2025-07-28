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
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        public InstructorRepository(MyNeoAcademyContext context) : base(context)
        {
        }


        public async Task<List<Instructor>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(i => i.Courses)
                .ToListAsync();
        }
        public async Task<Instructor?> GetByIdWithIncludesAsync(int id)  
        {
            return await Table
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.InstructorID == id);
        }

      
    }
}
