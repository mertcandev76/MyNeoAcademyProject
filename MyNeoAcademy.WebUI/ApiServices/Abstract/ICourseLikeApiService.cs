using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICourseLikeApiService
    {
        Task<List<ResultCourseLikeDTO>> GetAllAsync();
        Task<ResultCourseLikeDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCourseLikeDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetCourseDropdownItemsAsync();
        Task<List<SelectListItem>> GetUserDropdownItemsAsync();
        Task<List<ResultCourseLikeDTO>> GetByInstructorIdAsync();
    }
}
