using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICourseEnrollmentService : IGenericServiceWithoutUpdate<CourseEnrollment, CreateCourseEnrollmentDTO, ResultCourseEnrollmentDTO>
    {
        Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByCourseIdAsync(int courseId);
        Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByUserIdAsync(int appUserId);
        Task<int> GetEnrollmentCountByCourseIdAsync(int courseId);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<ResultCourseEnrollmentDTO>> GetAllWithIncludesAsync();
        Task<ResultCourseEnrollmentDTO?> GetByIdWithIncludesAsync(int id);
        Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByInstructorIdAsync(int instructorId);

    }
}

