using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IGenericService<Testimonial> _testimonialService;

        private readonly IMapper _mapper;

        public TestimonialsController(IGenericService<Testimonial> testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        //Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _testimonialService.TGetListAsync();
            return Ok(values);
        }
        //ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var values = await _testimonialService.TGetByIdAsync(id);
            return Ok(values);
        }
        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialDTO createTestimonialDTO)
        {
            var dtos = _mapper.Map<Testimonial>(createTestimonialDTO);
            await _testimonialService.TCreateAsync(dtos);
            return Ok("Yeni Referans Alanı  Oluşturuldu");
        }
        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTestimonialDTO  updateTestimonialDTO)
        {
            var dtos = _mapper.Map<Testimonial>(updateTestimonialDTO);
            await _testimonialService.TUpdateAsync(dtos);
            return Ok("Referans Alanı Güncellendi");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.TDeleteAsync(id);
            return Ok("Referans Alanı Silindi");
        }
    }
}
