using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using System.Security.Claims;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseLikesController : ControllerBase
    {
        private readonly ICourseLikeService _courseLikeService;

        public CourseLikesController(ICourseLikeService courseLikeService)
        {
            _courseLikeService = courseLikeService;
        }

        [HttpGet("byinstructor")]
        public async Task<IActionResult> GetByInstructorId()
        {
            var instructorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (instructorIdClaim == null || !int.TryParse(instructorIdClaim.Value, out int instructorId))
                return Unauthorized("InstructorId bilgisi bulunamadı veya geçersiz.");

            var filtered = await _courseLikeService.GetLikesByInstructorIdAsync(instructorId);

            return Ok(filtered);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var likes = await _courseLikeService.GetAllWithIncludesAsync();
            return Ok(likes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var like = await _courseLikeService.GetByIdWithIncludesAsync(id);
            if (like == null)
                return NotFound("Like not found.");

            return Ok(like);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseLikeDTO dto)
        {
            await _courseLikeService.CreateAsync(dto);
            return Ok("Like created successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseLikeService.DeleteByIdAsync(id);
            if (!deleted)
                return NotFound("Like not found.");

            return Ok("Like deleted successfully.");
        }
    }
}
