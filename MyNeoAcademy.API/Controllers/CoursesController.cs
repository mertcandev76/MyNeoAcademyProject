using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

    
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var courseList = await _courseService.GetAllWithCategoryAsync(); // Entity List<Blog>
            var dtos = _mapper.Map<List<ResultCourseDTO>>(courseList); // DTO List<ResultBlogDTO>
            return Ok(dtos); // DTO döndür
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courseService.GetByIdWithCategoryAsync(id);
            if (course == null) return NotFound();
            var dtos = _mapper.Map<ResultCourseDTO>(course);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDTO  createCourseDTO)
        {
            var dtos = _mapper.Map<Course>(createCourseDTO);
            await _courseService.CreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseDTO updateCourseDTO)
        {
            var dtos = _mapper.Map<Course>(updateCourseDTO);
            await _courseService.UpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        //Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var courses = await _courseService.GetByIdAsync(id);
            if (courses == null)
                return NotFound();

            await _courseService.DeleteAsync(courses);
            return Ok();
        }
    }
}
