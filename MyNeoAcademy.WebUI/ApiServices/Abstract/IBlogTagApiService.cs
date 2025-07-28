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

        // Ekstra: Belirli blogId ve tagId ile var mı kontrolü
        Task<bool> ExistsAsync(int blogId, int tagId);

    }
}
