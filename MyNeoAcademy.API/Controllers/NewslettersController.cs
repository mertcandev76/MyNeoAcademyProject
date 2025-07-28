using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Data;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;

        public NewslettersController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var newsletters = await _newsletterService.GetListAsync();
                return Ok(newsletters);
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
                var newsletter = await _newsletterService.GetByIdAsync(id);
                if (newsletter == null)
                    return NotFound("Kayıt bulunamadı.");

                return Ok(newsletter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNewsletterDTO dto)
        {
            try
            {
                await _newsletterService.CreateAsync(dto);
                return Ok("Abonelik başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateNewsletterDTO dto)
        {
            try
            {
                await _newsletterService.UpdateAsync(dto);
                return Ok("Abonelik bilgisi güncellendi.");
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
                var deleted = await _newsletterService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("Kayıt bulunamadı.");

                return Ok("Abonelik kaydı silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }

}

