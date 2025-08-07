using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs.Role;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class RoleManagerService : GenericManager<AppRole, CreateRoleDTO, UpdateRoleDTO, ResultRoleDTO>, IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IRepository<AppRole> _roleRepository;

        public RoleManagerService(
            RoleManager<AppRole> roleManager,
            IRepository<AppRole> roleRepository,
            IMapper mapper
        ) : base(roleRepository, mapper)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }

        public async Task<List<string>> GetRoleNamesAsync()
        {
            var roles = _roleManager.Roles
                .Where(r => r.Name != null)
                .Select(r => r.Name!)
                .ToList();

            return await Task.FromResult(roles); 
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {
                var newRole = new AppRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(), 
                    Description = $"{roleName} rolü sistemde tanımlandı."
                };

                var result = await _roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    throw new Exception($"Rol oluşturulurken hata: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return false;

            await _roleRepository.DeleteAsync(role);
            return true;
        }


        public async Task FixAllRoleNormalizationAsync()
        {
            var roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                if (!string.IsNullOrWhiteSpace(role.Name))
                {
                    var normalized = role.Name.ToUpper();
                    if (string.IsNullOrWhiteSpace(role.NormalizedName) || role.NormalizedName != normalized)
                    {
                        role.NormalizedName = normalized;
                        await _roleManager.UpdateAsync(role);
                    }
                }
            }
        }

    }
}
