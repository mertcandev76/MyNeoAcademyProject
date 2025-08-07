using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs.User;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAppUserApiService
    {
        Task<List<ResultAppUserDTO>> GetAllAsync();
        Task<ResultAppUserDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(UpdateAppUserDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> AssignRolesAsync(AssignRolesDTO dto);

        Task<List<SelectListItem>> GetDropdownItemsAsync();
        Task<List<SelectListItem>> GetDropdownItemsByRoleAsync(string roleName);
    }
}
