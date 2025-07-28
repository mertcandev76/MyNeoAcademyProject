using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.Abstract.MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IWebHostEnvironment _env;

        public InstructorsController(IInstructorService instructorService, IWebHostEnvironment env)
        {
            _instructorService = instructorService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var instructors = await _instructorService.GetAllWithIncludesAsync();
                return Ok(instructors);
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
                var instructor = await _instructorService.GetByIdWithIncludesAsync(id);
                if (instructor == null)
                    return NotFound("Eğitmen bulunamadı.");

                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CreateInstructorWithFileDTO dto)
        {
            try
            {
                await _instructorService.CreateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Yeni eğitmen kaydı oluşturuldu.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ekleme hatası: {ex.Message}");
            }
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromForm] UpdateInstructorWithFileDTO dto)
        {
            try
            {
                await _instructorService.UpdateWithFileAsync(dto, _env.WebRootPath);
                return Ok("Eğitmen güncellendi.");
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
                var deleted = await _instructorService.DeleteByIdAsync(id);
                if (!deleted)
                    return NotFound("Eğitmen bulunamadı.");

                return Ok("Eğitmen başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Silme hatası: {ex.Message}");
            }
        }
    }
}

