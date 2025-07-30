using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IAboutDetailApiService
    {
        Task<List<ResultAboutDetailDTO>> GetAllAsync();
        Task<ResultAboutDetailDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateAboutDetailDTO dto);
        Task<bool> UpdateAsync(UpdateAboutDetailDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
