using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.API.Utilities;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IGenericService<Testimonial> _testimonialService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TestimonialsController(IGenericService<Testimonial> testimonialService, IMapper mapper, IWebHostEnvironment env)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _testimonialService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultTestimonialDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _testimonialService.GetByIdAsync(id);
            if (entity == null) return NotFound("Referans bulunamadı.");
            var dto = _mapper.Map<ResultTestimonialDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateTestimonialWithFileDTO dto)
        {
            if (dto.ImageFile == null)
                return BadRequest("Bir profil fotoğrafı seçilmelidir.");

            string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/testimonials");
            var entity = _mapper.Map<Testimonial>(dto);
            entity.ImageUrl = imagePath;

            await _testimonialService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.TestimonialID }, "Yeni referans eklendi.");
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateTestimonialWithFileDTO dto)
        {
            var entity = await _testimonialService.GetByIdAsync(dto.TestimonialID);
            if (entity == null)
                return NotFound("Referans bulunamadı.");

            _mapper.Map(dto, entity);

            if (dto.ImageFile != null)
            {
                string imagePath = await FileHelper.SaveFileAsync(dto.ImageFile, _env.WebRootPath, "img/testimonials");
                entity.ImageUrl = imagePath;
            }

            await _testimonialService.UpdateAsync(entity);
            return Ok("Referans güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _testimonialService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Referans bulunamadı.");

            await _testimonialService.DeleteAsync(entity);
            return Ok("Referans silindi.");
        }
    }
}
