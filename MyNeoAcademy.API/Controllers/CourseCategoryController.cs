using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly IGenericService<CourseCategory> _courseCategoryService;
        private readonly IMapper _mapper;

        public CourseCategoryController(IGenericService<CourseCategory> courseCategoryService, IMapper mapper)
        {
            _courseCategoryService = courseCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await _courseCategoryService.TGetListAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _courseCategoryService.TGetByIdAsync(id);
            if (values == null) NotFound();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseCategoryDTO createCourseCategoryDTO)
        {
            var dtos = _mapper.Map<CourseCategory>(createCourseCategoryDTO);
            await _courseCategoryService.TCreateAsync(dtos);
            return Ok("Yeni Kategori Alanı Oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> Edit(UpdateCourseCategoryDTO updateCourseCategoryDTO)
        {
            var dtos = _mapper.Map<CourseCategory>(updateCourseCategoryDTO);
            await _courseCategoryService.TUpdateAsync(dtos);
            return Ok("Kategori Alanı Güncellendi.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseCategoryService.TDeleteAsync(id);
            return Ok("Kategori Alanı Silindi.");
        }

    }
}
