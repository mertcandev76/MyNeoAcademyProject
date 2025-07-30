using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class InstructorManager : GenericManager<
     Instructor,
     CreateInstructorDTO,
     UpdateInstructorDTO,
     ResultInstructorDTO>,
     IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InstructorManager(
            IInstructorRepository instructorRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(instructorRepository, mapper)
        {
            _instructorRepository = instructorRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";
        }

        public async Task<List<ResultInstructorDTO>> GetAllWithIncludesAsync()
        {
            var instructors = await _instructorRepository.GetAllWithIncludesAsync();
            var dtos = _mapper.Map<List<ResultInstructorDTO>>(instructors);

            var baseUrl = GetBaseUrl();

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }
            }

            return dtos;
        }

        public async Task<ResultInstructorDTO?> GetByIdWithIncludesAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdWithIncludesAsync(id);
            if (instructor == null)
                return null;

            var dto = _mapper.Map<ResultInstructorDTO>(instructor);
            var baseUrl = GetBaseUrl();

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
            {
                dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
            }

            return dto;
        }

        public async Task CreateWithFileAsync(CreateInstructorWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Eğitmen resmi zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/instructors");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateInstructorWithFileDTO dto, string webRootPath)
        {
            var entity = await _instructorRepository.GetByIdAsync(dto.InstructorID);
            if (entity == null)
                throw new Exception("Eğitmen bulunamadı.");

            if (dto.ImageFile != null)
            {
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/instructors");
            }

            _mapper.Map(dto, entity);
            await _instructorRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _instructorRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _instructorRepository.DeleteAsync(entity);
            return true;
        }
    }

}