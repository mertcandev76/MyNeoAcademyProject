using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IRecentBlogPostApiService
    {
        Task<List<ResultRecentBlogPostDTO>> GetAllAsync();
        Task<ResultRecentBlogPostDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateRecentBlogPostWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateRecentBlogPostWithFileDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
