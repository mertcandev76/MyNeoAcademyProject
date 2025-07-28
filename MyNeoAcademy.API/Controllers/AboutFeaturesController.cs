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
    public class AboutFeaturesController : ControllerBase
    {
        private readonly IAboutFeatureService _aboutFeatureService;

        public AboutFeaturesController(IAboutFeatureService aboutFeatureService)
        {
            _aboutFeatureService = aboutFeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var features = await _aboutFeatureService.GetAllWithIncludesAsync();
                return Ok(features);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var feature = await _aboutFeatureService.GetByIdWithIncludesAsync(id);
                if (feature == null)
                    return NotFound("Feature bulunamadı.");

                return Ok(feature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAboutFeatureDTO dto)
        {
            try
            {
                await _aboutFeatureService.CreateAsync(dto);
                return Ok("Yeni özellik başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAboutFeatureDTO dto)
        {
            try
            {
                await _aboutFeatureService.UpdateAsync(dto);
                return Ok("Özellik başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Güncelleme hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _aboutFeatureService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("Özellik bulunamadı.");

                return Ok("Özellik başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}
