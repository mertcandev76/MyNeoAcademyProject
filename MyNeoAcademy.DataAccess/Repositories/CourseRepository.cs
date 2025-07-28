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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<Course>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(c=>c.Category)
                .Include(c=>c.Instructor)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
              .Include(c => c.Category)
              .Include (c=>c.Instructor)    
              .FirstOrDefaultAsync(c=>c.CourseID==id);
        }
    }
}
