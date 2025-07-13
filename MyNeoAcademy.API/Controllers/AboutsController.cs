using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IGenericService<About> _aboutService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env; // Dosya kaydetmek için

        public AboutsController(IGenericService<About> aboutService, IMapper mapper, IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _mapper = mapper;
            _env = env;
        }

        //Listeleme(GET: api/About)
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var aboutList = await _aboutService.GetListAsync();
            return Ok(_mapper.Map<List<ResultAboutDTO>>(aboutList));
        }
        //ID ile Getirme(GET: api/About/5)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var abouts = await _aboutService.GetByIdAsync(id);
            if (abouts == null) return NotFound();
            return Ok(_mapper.Map<ResultAboutDTO>(abouts));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var abouts = await _aboutService.GetByIdAsync(id);
            if (abouts == null)
                return NotFound();

            await _aboutService.DeleteAsync(abouts);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAboutWithFileDTO dto)
        {
            var aboutEntity = _mapper.Map<About>(dto);

            // Dosyaları kaydet
            if (dto.ImageFrontFile != null)
            {
                var frontUrl = await SaveFileAsync(dto.ImageFrontFile);
                aboutEntity.ImageFrontUrl = frontUrl;
            }

            if (dto.ImageBackFile != null)
            {
                var backUrl = await SaveFileAsync(dto.ImageBackFile);
                aboutEntity.ImageBackUrl = backUrl;
            }

            await _aboutService.CreateAsync(aboutEntity);
            return Ok("Yeni Hakkımzda Alanı Oluşturuldu");
        }
        //Güncelleme(PUT: api/About/5)
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateAboutWithFileDTO dto)
        {
            var aboutEntity = await _aboutService.GetByIdAsync(dto.AboutID);
            if (aboutEntity == null)
                return NotFound();

            // Mevcut entity'yi güncelle (manuel yapabiliriz veya otomapper yeniden harcayabiliriz)
            aboutEntity.Subtitle = dto.Subtitle;
            aboutEntity.Title = dto.Title;
            aboutEntity.Description = dto.Description;
            aboutEntity.ButtonText = dto.ButtonText;
            aboutEntity.ButtonLink = dto.ButtonLink;

            if (dto.ImageFrontFile != null)
            {
                var frontUrl = await SaveFileAsync(dto.ImageFrontFile);
                aboutEntity.ImageFrontUrl = frontUrl;
            }
            // else varsa mevcut URL aynen kalsın

            if (dto.ImageBackFile != null)
            {
                var backUrl = await SaveFileAsync(dto.ImageBackFile);
                aboutEntity.ImageBackUrl = backUrl;
            }

            await _aboutService.UpdateAsync(aboutEntity);
            return Ok("Hakkımızda Alanı Güncellendi");
        }
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "abouts");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Kaydedilen dosyanın URL’si (örnek: /uploads/abouts/xyz.jpg)
            return $"/uploads/abouts/{uniqueFileName}";
        }
    }
}
