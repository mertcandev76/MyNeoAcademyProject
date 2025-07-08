using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class CourseManager:GenericManager<Course>,ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseManager(ICourseRepository courseRepository):base(courseRepository) 
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> GetAllWithCourseCategoryAsync() => await _courseRepository.GetAllWithCourseCategoryAsync();

        public async Task<Course?> GetByIdWithCourseCategoryAsync(int id) => await _courseRepository.GetByIdWithCourseCategoryAsync(id);
    }
}
