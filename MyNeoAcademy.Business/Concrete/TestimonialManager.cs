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
    public class TestimonialManager : GenericManager<Testimonial, CreateTestimonialDTO, UpdateTestimonialDTO, ResultTestimonialDTO>, ITestimonialService
    {
        private readonly IRepository<Testimonial> _testimonialRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestimonialManager(
            IRepository<Testimonial> testimonialRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(testimonialRepository, mapper)
        {
            _testimonialRepository = testimonialRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateWithFileAsync(CreateTestimonialWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Görsel dosyası zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/testimonials");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateTestimonialWithFileDTO dto, string webRootPath)
        {
            var entity = await _testimonialRepository.GetByIdAsync(dto.TestimonialID);
            if (entity == null)
                throw new Exception("Referans bulunamadı.");

            if (dto.ImageFile != null)
            {
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/testimonials");
            }

            _mapper.Map(dto, entity);
            await _testimonialRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _testimonialRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _testimonialRepository.DeleteAsync(entity);
            return true;
        }

        public override async Task<List<ResultTestimonialDTO>> GetListAsync()
        {
            var testimonials = await _testimonialRepository.GetListAsync();
            var dtos = _mapper.Map<List<ResultTestimonialDTO>>(testimonials);

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
            }

            return dtos;
        }

        public override async Task<ResultTestimonialDTO?> GetByIdAsync(int id)
        {
            var entity = await _testimonialRepository.GetByIdAsync(id);
            var dto = _mapper.Map<ResultTestimonialDTO>(entity);

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
            }

            return dto;
        }
    }
}
