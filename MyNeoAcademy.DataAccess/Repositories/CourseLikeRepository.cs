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
    public class CourseLikeRepository : GenericRepository<CourseLike>, ICourseLikeRepository
    {
        public CourseLikeRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }
        public async Task<List<CourseLike>> GetLikesByCourseIdAsync(int courseId)
        {
            return await Table
                .Where(l => l.CourseID == courseId)
                .Include(l => l.AppUser)
                .ToListAsync();
        }

        public async Task<List<CourseLike>> GetLikesByUserIdAsync(int appUserId)
        {
            return await Table
                .Where(l => l.AppUserID == appUserId)
                .Include(l => l.Course)
                .ToListAsync();
        }

        public async Task<int> GetLikeCountByCourseIdAsync(int courseId)
        {
            return await Table.CountAsync(l => l.CourseID == courseId);
        }
        public async Task<List<CourseLike>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(e => e.Course)
                .Include(e => e.AppUser)
                .ToListAsync();
        }
        public async Task<CourseLike?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                .Include(e => e.Course)
                .Include(e => e.AppUser)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<List<CourseLike>> GetLikesByInstructorIdAsync(int instructorId)
        {
            return await Table
                .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                .Include(e => e.AppUser)
                .Where(e => e.Course != null && e.Course.Instructor != null && e.Course.Instructor.AppUserID == instructorId)
                .ToListAsync();
        }
    }
}
