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

using Microsoft.AspNetCore.Http; 

namespace MyNeoAcademy.Business.Concrete
{
    public class AboutManager : GenericManager<About, CreateAboutDTO, UpdateAboutDTO, ResultAboutDTO>, IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor; 

        public AboutManager(
            IAboutRepository aboutRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor 
        ) : base(aboutRepository, mapper)
        {
            _aboutRepository = aboutRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<List<ResultAboutDTO>> GetAllWithIncludesAsync()
        {
            var aboutEntities = await _aboutRepository.GetAllWithIncludesAsync();
            var aboutDTOs = _mapper.Map<List<ResultAboutDTO>>(aboutEntities);

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            foreach (var dto in aboutDTOs)
            {
                if (!string.IsNullOrWhiteSpace(dto.ImageFrontUrl) && !dto.ImageFrontUrl.StartsWith("http"))
                    dto.ImageFrontUrl = $"{baseUrl}/{dto.ImageFrontUrl.TrimStart('/')}";

                if (!string.IsNullOrWhiteSpace(dto.ImageBackUrl) && !dto.ImageBackUrl.StartsWith("http"))
                    dto.ImageBackUrl = $"{baseUrl}/{dto.ImageBackUrl.TrimStart('/')}";
            }

            return aboutDTOs;
        }

        public async Task<ResultAboutDTO?> GetByIdWithIncludesAsync(int id)
        {
            var about = await _aboutRepository.GetByIdWithIncludesAsync(id);
            var dto = _mapper.Map<ResultAboutDTO>(about);

            if (dto != null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                string baseUrl;

                if (request != null && !string.IsNullOrEmpty(request.Host.Value))
                    baseUrl = $"{request.Scheme}://{request.Host}";
                else
                    baseUrl = "https://localhost:7230"; 

                if (!string.IsNullOrWhiteSpace(dto.ImageFrontUrl) && !dto.ImageFrontUrl.StartsWith("http"))
                {
                    dto.ImageFrontUrl = $"{baseUrl}/{dto.ImageFrontUrl.TrimStart('/')}";
                }

                if (!string.IsNullOrWhiteSpace(dto.ImageBackUrl) && !dto.ImageBackUrl.StartsWith("http"))
                {
                    dto.ImageBackUrl = $"{baseUrl}/{dto.ImageBackUrl.TrimStart('/')}";
                }
            }

            return dto;
        }





        public async Task CreateWithFileAsync(CreateAboutWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFrontFile == null || dto.ImageBackFile == null)
                throw new ArgumentException("Ön ve arka görseller zorunludur.");

            dto.ImageFrontUrl = await _fileService.SaveFileAsync(dto.ImageFrontFile, webRootPath, "img/abouts");
            dto.ImageBackUrl = await _fileService.SaveFileAsync(dto.ImageBackFile, webRootPath, "img/abouts");

            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateAboutWithFileDTO dto, string webRootPath)
        {
            var entity = await _aboutRepository.GetByIdAsync(dto.AboutID);
            if (entity == null)
                throw new Exception("About bulunamadı.");

            if (dto.ImageFrontFile != null)
                dto.ImageFrontUrl = await _fileService.SaveFileAsync(dto.ImageFrontFile, webRootPath, "img/abouts");

            if (dto.ImageBackFile != null)
                dto.ImageBackUrl = await _fileService.SaveFileAsync(dto.ImageBackFile, webRootPath, "img/abouts");

            _mapper.Map(dto, entity);
            await _aboutRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _aboutRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _aboutRepository.DeleteAsync(entity);
            return true;
        }
    }
}

