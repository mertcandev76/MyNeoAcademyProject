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
    public class SliderManager : GenericManager<Slider, CreateSliderDTO, UpdateSliderDTO, ResultSliderDTO>, ISliderService
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SliderManager(
            IRepository<Slider> sliderRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(sliderRepository, mapper)
        {
            _sliderRepository = sliderRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateWithFileAsync(CreateSliderWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Görsel dosyası zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/sliders");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateSliderWithFileDTO dto, string webRootPath)
        {
            var entity = await _sliderRepository.GetByIdAsync(dto.SliderID);
            if (entity == null)
                throw new Exception("Slider bulunamadı.");

            if (dto.ImageFile != null)
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/sliders");

            _mapper.Map(dto, entity);
            await _sliderRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _sliderRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _sliderRepository.DeleteAsync(entity);
            return true;
        }

        public override async Task<List<ResultSliderDTO>> GetListAsync()
        {
            var sliders = await _repository.GetListAsync();
            var dtos = _mapper.Map<List<ResultSliderDTO>>(sliders);

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


        public override async Task<ResultSliderDTO?> GetByIdAsync(int id)
        {
            var entity = await _sliderRepository.GetByIdAsync(id);
            var dto = _mapper.Map<ResultSliderDTO>(entity);

            if (dto != null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                    ? $"{request.Scheme}://{request.Host}"
                    : "https://localhost:7230";

                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
            }

            return dto;
        }
    }
}
