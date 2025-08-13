using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICourseEnrollmentApiService
    {
        Task<List<ResultCourseEnrollmentDTO>> GetAllAsync();
        Task<ResultCourseEnrollmentDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCourseEnrollmentDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetCourseDropdownItemsAsync();
        Task<List<SelectListItem>> GetUserDropdownItemsAsync();
        Task<List<ResultCourseEnrollmentDTO>> GetByInstructorIdAsync();
    }
}
