using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.Entity.Entities;
using MyNeoAcademy.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyNeoAcademy.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly MyNeoAcademyContext _context;
        private readonly IWebHostEnvironment _env;

        public AppUserManager(
            UserManager<AppUser> userManager,
            IMapper mapper,
            IFileService fileService,
            IWebHostEnvironment env, MyNeoAcademyContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileService = fileService;
            _env = env;
            _context = context;
        }






        public async Task<List<ResultAppUserDTO>> GetAllAsync()
        {
            var users = _userManager.Users
                 .Where(u => !u.IsDeleted)   // Silinmiş kullanıcıları hariç tut
                .ToList();

            var result = _mapper.Map<List<ResultAppUserDTO>>(users);

            foreach (var user in result)
            {
                var entity = await _userManager.FindByIdAsync(user.Id.ToString());
                if (entity != null)
                {
                    user.Roles = (await _userManager.GetRolesAsync(entity)).ToList();
                }
                else
                {
                    user.Roles = new List<string>();
                }
            }

            return result;
        }

        public async Task<ResultAppUserDTO?> GetByIdAsync(int id)
        {
            var user = await _userManager.Users
                .Where(u => !u.IsDeleted)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var dto = _mapper.Map<ResultAppUserDTO>(user);
            dto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return dto;
        }

        public async Task<bool> UpdateAsync(UpdateAppUserDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null) return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.UserName = dto.UserName;
            user.IsActive = dto.IsActive;

            if (dto.ProfileImageFile != null)
            {
                string folderPath = "uploads/profileimages";
                string webRootPath = _env.WebRootPath;

                var savedPath = await _fileService.SaveFileAsync(dto.ProfileImageFile, webRootPath, folderPath);
                user.ProfileImageUrl = "/" + savedPath.Replace("\\", "/");
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new List<string>();

            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<bool> AssignRolesAsync(AssignRolesDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null) return false;

            var existingRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!removeResult.Succeeded) return false;

            var addResult = await _userManager.AddToRolesAsync(user, dto.Roles);
            return addResult.Succeeded;
        }
        public async Task<List<ResultAppUserDTO>> GetUsersByRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            var result = _mapper.Map<List<ResultAppUserDTO>>(usersInRole);

            foreach (var user in result)
            {
                var entity = await _userManager.FindByIdAsync(user.Id.ToString());
                user.Roles = entity != null
                    ? (await _userManager.GetRolesAsync(entity)).ToList()
                    : new List<string>();
            }

            return result;
        }

    }
}

