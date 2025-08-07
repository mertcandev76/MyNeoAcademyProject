using MyNeoAcademy.Application.DTOs.Role;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IRoleApiService
    {
        Task<List<ResultRoleDTO>> GetAllAsync();
        Task CreateAsync(CreateRoleDTO dto);
        Task<bool> RoleExistsAsync(string roleName);
        Task UpdateAsync(UpdateRoleDTO dto);
        Task DeleteAsync(int id);
        Task<List<string>> GetRoleNamesAsync();
    }

}
