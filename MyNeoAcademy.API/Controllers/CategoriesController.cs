using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Otomatik model doğrulama sağlar
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericService<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericService<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoryList = await _categoryService.GetListAsync();
            var dtoList = _mapper.Map<List<ResultCategoryDTO>>(categoryList);
            return Ok(dtoList);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Kategori bulunamadı.");

            var dto = _mapper.Map<ResultCategoryDTO>(category);
            return Ok(dto);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            // FluentValidation kuralları buradan önce otomatik çalışır (400 döner)
            var entity = _mapper.Map<Category>(createCategoryDTO);
            await _categoryService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.CategoryID }, "Yeni kategori eklendi.");
        }

        // PUT: api/Categories
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            // FluentValidation burayı da otomatik kontrol eder
            var entity = _mapper.Map<Category>(updateCategoryDTO);
            await _categoryService.UpdateAsync(entity);
            return Ok("Kategori güncellendi.");
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(category);
            return Ok("Kategori silindi.");
        }
    }
}
