using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class CourseEnrollmentRepository : GenericRepository<CourseEnrollment>, ICourseEnrollmentRepository
    {
        public CourseEnrollmentRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }
        public async Task<List<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await Table
                .Where(e => e.CourseID == courseId)
                .Include(e => e.AppUser)
                .ToListAsync();
        }

        public async Task<List<CourseEnrollment>> GetEnrollmentsByUserIdAsync(int appUserId)
        {
            return await Table
                .Where(e => e.AppUserID == appUserId)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<int> GetEnrollmentCountByCourseIdAsync(int courseId)
        {
            return await Table.CountAsync(e => e.CourseID == courseId);
        }
        public async Task<List<CourseEnrollment>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(e => e.Course)
                .Include(e => e.AppUser)
                .ToListAsync();
        }
        public async Task<CourseEnrollment?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                .Include(e => e.Course)
                .Include(e => e.AppUser)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<List<CourseEnrollment>> GetEnrollmentsByInstructorIdAsync(int instructorId)
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

