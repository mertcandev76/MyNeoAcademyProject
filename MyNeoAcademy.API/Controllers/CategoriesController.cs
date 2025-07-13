using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericService<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericService<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoryList = await _categoryService.GetListAsync();
            var dtos = _mapper.Map<List<ResultCategoryDTO>>(categoryList);
            return Ok(dtos);
        }

        // ID ile Getirme
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            var dto = _mapper.Map<ResultCategoryDTO>(category);
            return Ok(dto);
        }

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO createCategoryDTO)
        {
            var entity = _mapper.Map<Category>(createCategoryDTO);
            await _categoryService.CreateAsync(entity);
            return Ok("Yeni kategori eklendi");
        }

        // Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            var entity = _mapper.Map<Category>(updateCategoryDTO);
            await _categoryService.UpdateAsync(entity);
            return Ok("Kategori güncellendi");
        }

        // Silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(category);
            return Ok("Kategori silindi");
        }
    }
}
