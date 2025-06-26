using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IGenericService<Course> _courseService;
        private readonly IMapper _mapper;
        public CourseController(IGenericService<Course> courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _courseService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _courseService.TGetByIdAsync(id);
            if (values == null) NotFound();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDTO  createCourseDTO)
        {
            var dtos = _mapper.Map<Course>(createCourseDTO);
            await _courseService.TCreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateCourseDTO updateCourseDTO)
        {
            var dtos = _mapper.Map<Course>(updateCourseDTO);
            await _courseService.TUpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.TDeleteAsync(id);
            return Ok("Kategori Alanı Silindi.");
        }
    }
}
