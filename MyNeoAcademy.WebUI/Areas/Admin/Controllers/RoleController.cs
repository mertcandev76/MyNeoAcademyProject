using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs.Role;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using System.Data;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class RoleController : Controller
    {
        private readonly IRoleApiService _roleApiService;

        public RoleController(IRoleApiService roleApiService)
        {
            _roleApiService = roleApiService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleApiService.GetAllAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var exists = await _roleApiService.RoleExistsAsync(dto.Name);
            if (exists)
            {
                ModelState.AddModelError("", "Bu rol zaten mevcut.");
                return View(dto);
            }

            await _roleApiService.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var roles = await _roleApiService.GetAllAsync();
            var role = roles.FirstOrDefault(r => r.Id == id);
            if (role == null)
                return NotFound();

            var dto = new UpdateRoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoleDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _roleApiService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roleApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
