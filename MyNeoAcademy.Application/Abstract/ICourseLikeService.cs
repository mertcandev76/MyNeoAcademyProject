using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICourseLikeService : IGenericServiceWithoutUpdate<CourseLike, CreateCourseLikeDTO, ResultCourseLikeDTO>
    {
        Task<List<ResultCourseLikeDTO>> GetLikesByCourseIdAsync(int courseId);
        Task<List<ResultCourseLikeDTO>> GetLikesByUserIdAsync(int appUserId);
        Task<int> GetLikeCountByCourseIdAsync(int courseId);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<ResultCourseLikeDTO>> GetAllWithIncludesAsync();
        Task<ResultCourseLikeDTO?> GetByIdWithIncludesAsync(int id);
        Task<List<ResultCourseLikeDTO>> GetLikesByInstructorIdAsync(int instructorId);
    }
}

