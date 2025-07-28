using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICategoryApiService
    {
        Task<List<ResultCategoryDTO>> GetAllAsync();
        Task<ResultCategoryDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCategoryDTO dto);
        Task<bool> UpdateAsync(UpdateCategoryDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}
