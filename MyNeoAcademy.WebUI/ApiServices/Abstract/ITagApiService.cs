using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ITagApiService
    {
        Task<List<ResultTagDTO>> GetAllAsync();
        Task<ResultTagDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateTagDTO dto);
        Task<bool> UpdateAsync(UpdateTagDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}
