using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using MyNeoAcademy.Entity.Entities;
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
        private readonly IValidator<CreateSliderWithFileDTO> _createValidator;
        private readonly IValidator<UpdateSliderWithFileDTO> _updateValidator;

        public SlidersController(IGenericService<Slider> sliderService,
                                 IMapper mapper,
                                 IWebHostEnvironment env,
                                 IValidator<CreateSliderWithFileDTO> createValidator,
                                 IValidator<UpdateSliderWithFileDTO> updateValidator)
        {
            _sliderService = sliderService;
            _mapper = mapper;
            _env = env;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sliderList = await _sliderService.GetListAsync();
            var dtos = _mapper.Map<List<ResultSliderDTO>>(sliderList);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null) return NotFound();
            var dto = _mapper.Map<ResultSliderDTO>(slider);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSliderWithFileDTO dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var sliderEntity = _mapper.Map<Slider>(dto);

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var imageUrl = await SaveFileAsync(dto.ImageFile);
                sliderEntity.ImageUrl = imageUrl;
            }

            await _sliderService.CreateAsync(sliderEntity);
            return Ok("Yeni slider eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSliderWithFileDTO dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var existingSlider = await _sliderService.GetByIdAsync(dto.SliderID);
            if (existingSlider == null)
                return NotFound("Slider bulunamadı.");

            // Güncelle
            existingSlider.Title = dto.Title;
            existingSlider.SubTitle = dto.SubTitle;
            existingSlider.ButtonText = dto.ButtonText;
            existingSlider.ButtonUrl = dto.ButtonUrl;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var imageUrl = await SaveFileAsync(dto.ImageFile);
                existingSlider.ImageUrl = imageUrl;
            }

            await _sliderService.UpdateAsync(existingSlider);
            return Ok("Slider güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null) return NotFound();

            await _sliderService.DeleteAsync(slider);
            return Ok("Slider silindi");
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
            var uploadsFolder = Path.Combine(webRoot, "uploads", "sliders");

            Directory.CreateDirectory(uploadsFolder); // klasör varsa bir şey yapmaz

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/sliders/{uniqueFileName}";
        }
    }
}
