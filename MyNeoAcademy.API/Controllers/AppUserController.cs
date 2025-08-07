using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.User;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        // GET: api/AppUser
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _appUserService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/AppUser/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _appUserService.GetByIdAsync(id);
            if (user == null)
                return NotFound($"Kullanıcı (ID: {id}) bulunamadı.");

            return Ok(user);
        }

        // PUT: api/AppUser
        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] UpdateAppUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _appUserService.UpdateAsync(dto);
            if (!success)
                return BadRequest("Kullanıcı güncellenemedi.");

            return Ok("Kullanıcı başarıyla güncellendi.");
        }

        // DELETE: api/AppUser/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _appUserService.DeleteAsync(id);
            if (!success)
                return NotFound("Kullanıcı bulunamadı veya silinemedi.");

            return Ok("Kullanıcı başarıyla silindi.");
        }

        // POST: api/AppUser/AssignRoles
        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRolesDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _appUserService.AssignRolesAsync(dto);
            if (!success)
                return BadRequest("Roller atanamadı.");

            return Ok("Roller başarıyla atandı.");
        }

        // GET: api/AppUser/ByRole/Author
        [HttpGet("ByRole/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _appUserService.GetUsersByRoleAsync(roleName);
            return Ok(users);
        }

    }
}
