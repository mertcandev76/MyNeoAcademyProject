using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface INewsletterApiService
    {
        Task<List<ResultNewsletterDTO>> GetAllAsync();
        Task<ResultNewsletterDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateNewsletterDTO dto);
        Task<bool> UpdateAsync(UpdateNewsletterDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
