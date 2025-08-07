using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.Role;
using System.Data;

namespace MyNeoAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetListAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _roleService.RoleExistsAsync(dto.Name))
                return Conflict("Bu rol zaten mevcut.");

            await _roleService.CreateAsync(dto);
            return Ok("Rol başarıyla oluşturuldu.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id uyuşmuyor.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingRole = await _roleService.GetByIdAsync(id);
            if (existingRole == null)
                return NotFound("Rol bulunamadı.");

            await _roleService.UpdateAsync(dto);
            return Ok("Rol başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _roleService.DeleteByIdAsync(id);
            if (!success)
                return NotFound("Silinecek rol bulunamadı.");

            return Ok("Rol başarıyla silindi.");
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetRoleNames()
        {
            var roleNames = await _roleService.GetRoleNamesAsync();
            return Ok(roleNames);
        }


        [HttpPost("fix-normalization")]
        public async Task<IActionResult> FixRoleNormalization()
        {
            await _roleService.FixAllRoleNormalizationAsync();
            return Ok("Tüm rollerin NormalizedName alanları güncellendi.");
        }
    }

}
