using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IInstructorApiService
    {
        Task<List<ResultInstructorDTO>> GetAllAsync();
        Task<ResultInstructorDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateInstructorWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateInstructorWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}
