using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutDetailsController : ControllerBase
    {
        private readonly IAboutDetailService _aboutDetailService;

        public AboutDetailsController(IAboutDetailService aboutDetailService)
        {
            _aboutDetailService = aboutDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var detaylar = await _aboutDetailService.GetListAsync();
                return Ok(detaylar);
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
                var detay = await _aboutDetailService.GetByIdAsync(id);
                if (detay == null)
                    return NotFound("Kayıt bulunamadı.");

                return Ok(detay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAboutDetailDTO dto)
        {
            try
            {
                await _aboutDetailService.CreateAsync(dto);
                return Ok("Hakkımızda detayı başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAboutDetailDTO dto)
        {
            try
            {
                await _aboutDetailService.UpdateAsync(dto);
                return Ok("Hakkımızda detayı başarıyla güncellendi.");
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
                var silindi = await _aboutDetailService.DeleteByIdAsync(id);
                if (!silindi)
                    return NotFound("Kayıt bulunamadı.");

                return Ok("Hakkımızda detayı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}
