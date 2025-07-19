using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Otomatik model doğrulama sağlar
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _categoryService.GetAllWithBlogAsync();
            var dtoList = _mapper.Map<List<ResultCategoryDTO>>(list);
            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var entity = await _categoryService.GetByIdWithBlogAsync(id);
            if (entity == null) return NotFound("Kategori bulunamadı.");
            var dto = _mapper.Map<ResultCategoryDTO>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _categoryService.CreateAsync(entity);
            return CreatedAtAction(nameof(Detail), new { id = entity.CategoryID }, "Yeni kategori eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO dto)
        {
            var entity = await _categoryService.GetByIdAsync(dto.CategoryID);
            if (entity == null)
                return NotFound("Kategori bulunamadı.");

            _mapper.Map(dto, entity); 

            await _categoryService.UpdateAsync(entity);
            return Ok("Kategori güncellendi.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _categoryService.GetByIdAsync(id);
            if (entity == null)
                return NotFound("Kategori bulunamadı.");

            await _categoryService.DeleteAsync(entity);
            return Ok("Kategori silindi.");
        }
    }
}
