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

        public async Task<List<Course>> GetAllWithCourseCategoryAsync()
        {
            return await Table
                .Include(c=>c.CourseCategory)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdWithCourseCategoryAsync(int id)
        {
            return await Table
              .Include(c => c.CourseCategory)
              .FirstOrDefaultAsync(c=>c.CourseID==id);
        }
    }
}
