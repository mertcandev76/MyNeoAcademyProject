using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICourseApiService
    {
        Task<List<ResultCourseDTO>> GetAllAsync();
        Task<ResultCourseDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCourseWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateCourseWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}
