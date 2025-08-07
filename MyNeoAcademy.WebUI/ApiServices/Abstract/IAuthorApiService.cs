using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAuthorApiService
    {
        Task<List<ResultAuthorDTO>> GetAllAsync();
        Task<ResultAuthorDTO?> GetByIdAsync(int id);
        Task<ResultAuthorDTO?> GetByAppUserIdAsync(int appUserId);
        Task<bool> CreateAsync(CreateAuthorWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateAuthorWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}

