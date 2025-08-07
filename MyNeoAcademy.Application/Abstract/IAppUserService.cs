using MyNeoAcademy.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAppUserService
    {
        Task<List<ResultAppUserDTO>> GetAllAsync();
        Task<ResultAppUserDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(UpdateAppUserDTO dto);
        Task<bool> DeleteAsync(int id); 
        Task<List<string>> GetUserRolesAsync(int userId);
        Task<bool> AssignRolesAsync(AssignRolesDTO dto);
        Task<List<ResultAppUserDTO>> GetUsersByRoleAsync(string roleName);

    }
}
