using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoriesController : ControllerBase
    {
        private readonly IGenericService<CourseCategory> _courseCategoryService;
        private readonly IMapper _mapper;

        public CourseCategoriesController(IGenericService<CourseCategory> courseCategoryService, IMapper mapper)
        {
            _courseCategoryService = courseCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var courseCategoryList = await _courseCategoryService.GetListAsync(); // Entity List<Blog>
            var dtos = _mapper.Map<List<ResultCourseCategoryDTO>>(courseCategoryList); // DTO List<ResultBlogDTO>
            return Ok(dtos); // DTO döndür
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var courseCategories = await _courseCategoryService.GetByIdAsync(id);
            if (courseCategories == null) return NotFound();
            var dtos = _mapper.Map<ResultCourseCategoryDTO>(courseCategories);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseCategoryDTO createCourseCategoryDTO)
        {
            var dtos = _mapper.Map<CourseCategory>(createCourseCategoryDTO);
            await _courseCategoryService.CreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateCourseCategoryDTO updateCourseCategoryDTO)
        {
            var dtos = _mapper.Map<CourseCategory>(updateCourseCategoryDTO);
            await _courseCategoryService.UpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var courseCategories = await _courseCategoryService.GetByIdAsync(id);
            if (courseCategories == null)
                return NotFound();

            await _courseCategoryService.DeleteAsync(courseCategories);
            return Ok();
        }

    }
}
