using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAboutFeatureApiService
    {
        Task<List<ResultAboutFeatureDTO>> GetAllAsync();
        Task<ResultAboutFeatureDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateAboutFeatureDTO dto);
        Task<bool> UpdateAsync(UpdateAboutFeatureDTO dto);
        Task<bool> DeleteAsync(int id);

    }
}
