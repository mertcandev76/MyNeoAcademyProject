using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ICourseEnrollmentRepository : IRepository<CourseEnrollment>
    {
        Task<List<CourseEnrollment>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<List<CourseEnrollment>> GetEnrollmentsByUserIdAsync(int appUserId);
        Task<int> GetEnrollmentCountByCourseIdAsync(int courseId);
        Task<List<CourseEnrollment>> GetAllWithIncludesAsync();
        Task<CourseEnrollment?> GetByIdWithIncludesAsync(int id);
        Task<List<CourseEnrollment>> GetEnrollmentsByInstructorIdAsync(int instructorId);
    }
}
