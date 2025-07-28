using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ITestimonialApiService
    {
        Task<List<ResultTestimonialDTO>> GetAllAsync();
        Task<ResultTestimonialDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateTestimonialWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateTestimonialWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
