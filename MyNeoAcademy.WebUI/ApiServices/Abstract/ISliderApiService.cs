using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ISliderApiService
    {
        Task<List<ResultSliderDTO>> GetAllAsync();
        Task<ResultSliderDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateSliderWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateSliderWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
