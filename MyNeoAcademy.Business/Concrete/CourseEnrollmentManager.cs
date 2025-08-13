using AutoMapper;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class CourseEnrollmentManager : GenericManagerWithoutUpdate<CourseEnrollment, CreateCourseEnrollmentDTO, ResultCourseEnrollmentDTO>, ICourseEnrollmentService
    {
        private readonly ICourseEnrollmentRepository _courseEnrollmentRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseEnrollmentManager(
            ICourseEnrollmentRepository courseEnrollmentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
            : base(courseEnrollmentRepository, mapper)
        {
            _courseEnrollmentRepository = courseEnrollmentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            var enrollments = await _courseEnrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
            return _mapper.Map<List<ResultCourseEnrollmentDTO>>(enrollments);
        }

        public async Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByUserIdAsync(int appUserId)
        {
            var enrollments = await _courseEnrollmentRepository.GetEnrollmentsByUserIdAsync(appUserId);
            return _mapper.Map<List<ResultCourseEnrollmentDTO>>(enrollments);
        }

        public async Task<int> GetEnrollmentCountByCourseIdAsync(int courseId)
        {
            return await _courseEnrollmentRepository.GetEnrollmentCountByCourseIdAsync(courseId);
        }


        public override async Task CreateAsync(CreateCourseEnrollmentDTO dto)
        {
            var entity = _mapper.Map<CourseEnrollment>(dto);
            await _courseEnrollmentRepository.CreateAsync(entity);
            var course = await _courseRepository.GetByIdAsync(dto.CourseID);
            if (course != null)
            {
                course.StudentCount += 1;
                await _courseRepository.UpdateAsync(course);
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _courseEnrollmentRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _courseEnrollmentRepository.DeleteAsync(entity);
            var course = await _courseRepository.GetByIdAsync(entity.CourseID);
            if (course != null && course.StudentCount > 0)
            {
                course.StudentCount -= 1;
                await _courseRepository.UpdateAsync(course);
            }

            return true;
        }
        public async Task<List<ResultCourseEnrollmentDTO>> GetAllWithIncludesAsync()
        {
            var enrollments = await _courseEnrollmentRepository.GetAllWithIncludesAsync();
            return _mapper.Map<List<ResultCourseEnrollmentDTO>>(enrollments);
        }
        public async Task<ResultCourseEnrollmentDTO?> GetByIdWithIncludesAsync(int id)
        {
            var enrollment = await _courseEnrollmentRepository.GetByIdWithIncludesAsync(id);
            return enrollment == null ? null : _mapper.Map<ResultCourseEnrollmentDTO>(enrollment);
        }
        public async Task<List<ResultCourseEnrollmentDTO>> GetEnrollmentsByInstructorIdAsync(int instructorId)
        {
            var enrollments = await _courseEnrollmentRepository.GetEnrollmentsByInstructorIdAsync(instructorId);
            return _mapper.Map<List<ResultCourseEnrollmentDTO>>(enrollments);
        }



    }
}

