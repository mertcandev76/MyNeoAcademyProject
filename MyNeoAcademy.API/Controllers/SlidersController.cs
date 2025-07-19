using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;
using MyNeoAcademy.API.Utilities;
using FluentValidation;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly IGenericService<Slider> _sliderService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SlidersController(IGenericService<Slider> sliderService, IMapper mapper, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _sliderService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultSliderDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _sliderService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Slider bulunamadı.");

            var dto = _mapper.Map<ResultSliderDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateSliderWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Bir görsel dosyası seçilmelidir.");

            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/sliders");
            var entity = _mapper.Map<Slider>(dto);
            entity.ImageUrl = imagePath;

            await _sliderService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.SliderID }, "Yeni Slider eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateSliderWithFileDTO dto)
        {
            var entity = await _sliderService.GetByIdAsync(dto.SliderID);
            if (entity == null)
                return NotFound("Slider bulunamadı.");

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                var imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/sliders");
                entity.ImageUrl = imagePath;
            }

            // Update metodu mevcut entity ile çağrılıyor
            await _sliderService.UpdateAsync(entity);

            return Ok("Slider güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _sliderService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Slider bulunamadı.");

            await _sliderService.DeleteAsync(entity);
            return Ok("Slider silindi.");
        }
    }
}
