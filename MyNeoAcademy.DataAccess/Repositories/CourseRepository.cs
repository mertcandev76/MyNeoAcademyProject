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
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .Include(c => c.Comments)
                .Include(c => c.CourseEnrollments)
                .Include(c => c.CourseLikes)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .Include(c => c.Comments)
                .Include(c => c.CourseEnrollments)
                .Include(c => c.CourseLikes)
                .FirstOrDefaultAsync(c => c.CourseID == id);
        }
        public async Task<List<Course>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            return await Table
                .Where(c => c.InstructorID == instructorId)
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                .Include(c => c.Comments)
                .Include(c => c.CourseEnrollments)
                .Include(c => c.CourseLikes)
                .ToListAsync();
        }

    }
}
