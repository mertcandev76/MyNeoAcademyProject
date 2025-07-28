using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAboutApiService
    {
        Task<List<ResultAboutDTO>> GetAllAsync();
        Task<ResultAboutDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateAboutWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateAboutWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<List<SelectListItem>> GetDropdownItemsAsync();
    }
}
