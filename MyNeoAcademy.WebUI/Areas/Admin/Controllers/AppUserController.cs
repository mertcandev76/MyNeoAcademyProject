using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.ApiServices.Concrete;
using MyNeoAcademy.WebUI.Areas.Admin.Models;
using System.Data;

namespace MyNeoAcademy.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class AppUserController : Controller
    {
        private readonly IAppUserApiService _appUserApiService;
        private readonly IRoleApiService _roleApiService;

        public AppUserController(IAppUserApiService appUserApiService, IRoleApiService roleApiService)
        {
            _appUserApiService = appUserApiService;
            _roleApiService = roleApiService;
        }


        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _appUserApiService.GetAllAsync();
            return View(users);
        }


        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _appUserApiService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var vm = new UpdateAppUserViewModel
            {
                UserDto = new UpdateAppUserDTO
                {
                    Id = user.Id,
                    FirstName = user.FullName.Split(' ')[0],
                    LastName = user.FullName.Split(' ').Length > 1 ? user.FullName.Split(' ')[1] : string.Empty,
                    Email = user.Email,
                    UserName = user.UserName,
                    IsActive = user.IsActive
                },
                ProfileImageUrl = user.ProfileImageUrl
            };

            return View(vm);
        }


        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAppUserViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (id != vm.UserDto.Id)
                return BadRequest("Id uyuşmuyor.");

            var success = await _appUserApiService.UpdateAsync(vm.UserDto);
            if (!success)
            {
                ModelState.AddModelError("", "Kullanıcı güncellenemedi.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var user = await _appUserApiService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }


        [HttpGet("AssignRoles/{id}")]
        public async Task<IActionResult> AssignRoles(int id)
        {
            var user = await _appUserApiService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var allRoles = await _roleApiService.GetAllAsync();

            var model = new AssignRolesDTO
            {
                UserId = user.Id,
                Roles = user.Roles ?? new List<string>() 
            };

            ViewBag.AllRoles = allRoles;
            ViewBag.UserName = user.UserName;

            return View(model);
        }


        [HttpPost("AssignRoles/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles(AssignRolesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await _roleApiService.GetAllAsync();
                ViewBag.AllRoles = allRoles;
                ViewBag.UserName = "Kullanıcı"; 
                return View(dto);
            }

            var success = await _appUserApiService.AssignRolesAsync(dto);
            if (!success)
            {
                ModelState.AddModelError("", "Roller atanamadı.");
                var allRoles = await _roleApiService.GetAllAsync();
                ViewBag.AllRoles = allRoles;
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _appUserApiService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}





