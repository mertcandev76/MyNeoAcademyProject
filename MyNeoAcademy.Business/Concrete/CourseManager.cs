using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class CourseManager : GenericManager<Course, CreateCourseDTO, UpdateCourseDTO, ResultCourseDTO>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseManager(
            ICourseRepository courseRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(courseRepository, mapper)
        {
            _courseRepository = courseRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<ResultCourseDTO>> GetAllWithIncludesAsync()
        {
            var courses = await _courseRepository.GetAllWithIncludesAsync();
            var dtos = _mapper.Map<List<ResultCourseDTO>>(courses);

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }
                dto.ReviewCount = dto.Comments?.Count ?? 0;
                dto.StudentCount = dto.Enrollments?.Count ?? 0;
                dto.LikeCount = dto.Likes?.Count ?? 0;
                if (dto.Comments != null && dto.Comments.Count > 0)
                    dto.Rating = (int)dto.Comments.Average(c => c.Rating);
                else
                    dto.Rating = 0;
            }

            return dtos;
        }

        public async Task<ResultCourseDTO?> GetByIdWithIncludesAsync(int id)
        {
            var course = await _courseRepository.GetByIdWithIncludesAsync(id);
            var dto = _mapper.Map<ResultCourseDTO>(course);

            if (dto != null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                    ? $"{request.Scheme}://{request.Host}"
                    : "https://localhost:7230";

                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }

                // Burada da hesaplanan alanları ekle:
                dto.ReviewCount = dto.Comments?.Count ?? 0;
                dto.StudentCount = dto.Enrollments?.Count ?? 0;
                dto.LikeCount = dto.Likes?.Count ?? 0;

                if (dto.Comments != null && dto.Comments.Count > 0)
                    dto.Rating = (int)dto.Comments.Average(c => c.Rating);
                else
                    dto.Rating = 0;
            }

            return dto;
        }

        public async Task CreateWithFileAsync(CreateCourseWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Görsel zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/courses");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateCourseWithFileDTO dto, string webRootPath)
        {
            var entity = await _courseRepository.GetByIdAsync(dto.CourseID);
            if (entity == null)
                throw new Exception("Course bulunamadı.");

            if (dto.ImageFile != null)
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/courses");

            _mapper.Map(dto, entity);
            await _courseRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _courseRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _courseRepository.DeleteAsync(entity);
            return true;
        }
        public async Task<List<ResultCourseDTO>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            var courses = await _courseRepository.GetCoursesByInstructorIdAsync(instructorId);
            var dtos = _mapper.Map<List<ResultCourseDTO>>(courses);

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }

                dto.ReviewCount = dto.Comments?.Count ?? 0;
                dto.StudentCount = dto.Enrollments?.Count ?? 0;
                dto.LikeCount = dto.Likes?.Count ?? 0;

                if (dto.Comments != null && dto.Comments.Count > 0)
                    dto.Rating = (int)dto.Comments.Average(c => c.Rating);
                else
                    dto.Rating = 0;
            }

            return dtos;
        }

    }
}
