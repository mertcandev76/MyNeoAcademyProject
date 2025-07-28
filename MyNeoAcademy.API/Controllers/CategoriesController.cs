using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _categoryService.GetAllWithIncludesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Veriler alınırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdWithIncludesAsync(id);
                return category is not null
                    ? Ok(category)
                    : NotFound("Kategori bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDTO dto)
        {
            try
            {
                await _categoryService.CreateAsync(dto);
                return Created(string.Empty, "Kategori başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Oluşturma sırasında bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryDTO dto)
        {
            try
            {
                await _categoryService.UpdateAsync(dto);
                return Ok("Kategori başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ex.Message == "Entity not found"
                    ? NotFound("Kategori bulunamadı.")
                    : StatusCode(500, $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _categoryService.DeleteByIdAsync(id);
                return success
                    ? Ok("Kategori başarıyla silindi.")
                    : NotFound("Kategori bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme sırasında bir hata oluştu: {ex.Message}");
            }
        }
    }
}