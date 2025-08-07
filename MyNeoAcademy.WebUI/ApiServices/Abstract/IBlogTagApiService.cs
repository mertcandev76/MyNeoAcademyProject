using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface IBlogTagApiService
    {
        Task<List<ResultBlogTagDTO>> GetAllAsync();
        Task<ResultBlogTagDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateBlogTagDTO dto);
        Task<bool> UpdateAsync(UpdateBlogTagDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int blogId, int tagId);

    }
}
