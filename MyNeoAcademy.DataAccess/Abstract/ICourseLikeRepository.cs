using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ICourseLikeRepository : IRepository<CourseLike>
    {
        Task<List<CourseLike>> GetLikesByCourseIdAsync(int courseId);
        Task<List<CourseLike>> GetLikesByUserIdAsync(int appUserId);
        Task<int> GetLikeCountByCourseIdAsync(int courseId);
        Task<List<CourseLike>> GetAllWithIncludesAsync();
        Task<CourseLike?> GetByIdWithIncludesAsync(int id);
        Task<List<CourseLike>> GetLikesByInstructorIdAsync(int instructorId);
    }
}
