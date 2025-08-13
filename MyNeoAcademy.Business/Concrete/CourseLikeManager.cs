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
    public class CourseLikeManager : GenericManagerWithoutUpdate<CourseLike, CreateCourseLikeDTO, ResultCourseLikeDTO>, ICourseLikeService
    {
        private readonly ICourseLikeRepository _courseLikeRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseLikeManager(
            ICourseLikeRepository courseLikeRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
            : base(courseLikeRepository, mapper)
        {
            _courseLikeRepository = courseLikeRepository;
            _courseRepository = courseRepository;
        }

        public async Task<List<ResultCourseLikeDTO>> GetLikesByCourseIdAsync(int courseId)
        {
            var likes = await _courseLikeRepository.GetLikesByCourseIdAsync(courseId);
            return _mapper.Map<List<ResultCourseLikeDTO>>(likes);
        }

        public async Task<List<ResultCourseLikeDTO>> GetLikesByUserIdAsync(int appUserId)
        {
            var likes = await _courseLikeRepository.GetLikesByUserIdAsync(appUserId);
            return _mapper.Map<List<ResultCourseLikeDTO>>(likes);
        }

        public async Task<int> GetLikeCountByCourseIdAsync(int courseId)
        {
            return await _courseLikeRepository.GetLikeCountByCourseIdAsync(courseId);
        }

        public override async Task CreateAsync(CreateCourseLikeDTO dto)
        {
            var entity = _mapper.Map<CourseLike>(dto);
            await _courseLikeRepository.CreateAsync(entity);
            var course = await _courseRepository.GetByIdAsync(dto.CourseID);
            if (course != null)
            {
                course.LikeCount += 1;
                await _courseRepository.UpdateAsync(course);
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _courseLikeRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _courseLikeRepository.DeleteAsync(entity);
            var course = await _courseRepository.GetByIdAsync(entity.CourseID);
            if (course != null && course.LikeCount > 0)
            {
                course.LikeCount -= 1;
                await _courseRepository.UpdateAsync(course);
            }

            return true;
        }
        public async Task<List<ResultCourseLikeDTO>> GetAllWithIncludesAsync()
        {
            var enrollments = await _courseLikeRepository.GetAllWithIncludesAsync();
            return _mapper.Map<List<ResultCourseLikeDTO>>(enrollments);
        }
        public async Task<ResultCourseLikeDTO?> GetByIdWithIncludesAsync(int id)
        {
            var enrollment = await _courseLikeRepository.GetByIdWithIncludesAsync(id);
            return enrollment == null ? null : _mapper.Map<ResultCourseLikeDTO>(enrollment);
        }
        public async Task<List<ResultCourseLikeDTO>> GetLikesByInstructorIdAsync(int instructorId)
        {
            var enrollments = await _courseLikeRepository.GetLikesByInstructorIdAsync(instructorId);
            return _mapper.Map<List<ResultCourseLikeDTO>>(enrollments);
        }
    }
}

