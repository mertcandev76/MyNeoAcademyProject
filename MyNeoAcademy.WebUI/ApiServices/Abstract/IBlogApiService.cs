using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IBlogApiService
    {
        Task<List<ResultBlogDTO>> GetAllAsync();
        Task<ResultBlogDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateBlogWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateBlogWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync(); // Dropdown için
    }
}
