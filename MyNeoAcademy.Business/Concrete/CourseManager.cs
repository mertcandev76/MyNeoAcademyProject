using MyNeoAcademy.Application.Abstract;
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

        public async Task<List<Course>> GetAllWithCategoryAndInstructorAsync() => await _courseRepository.GetAllWithCategoryAndInstructorAsync();

        public async Task<Course?> GetByIdWithCategoryAndInstructorAsync(int id) => await _courseRepository.GetByIdWithCategoryAndInstructorAsync(id);
    }
}
