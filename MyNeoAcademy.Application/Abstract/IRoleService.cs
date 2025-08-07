using MyNeoAcademy.Application.DTOs.Role;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IRoleService : IGenericService<
          AppRole,
          CreateRoleDTO,
          UpdateRoleDTO,
          ResultRoleDTO>
    {
        Task<List<string>> GetRoleNamesAsync();
        Task<bool> RoleExistsAsync(string roleName);
        Task CreateRoleAsync(string roleName);
        Task<bool> DeleteByIdAsync(int id);
        Task FixAllRoleNormalizationAsync();
    }
}
