using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICommentApiService
    {
        Task<List<ResultCommentDTO>> GetAllCourseCommentsAsync(int courseId);

        Task<PagedResultDTO<ResultCommentDTO>> GetPagedAsync(int page, int pageSize);
        Task<PagedResultDTO<ResultCommentDTO>> GetPagedByBlogAsync(int blogId, int page, int pageSize);
        Task<List<ResultCommentDTO>> GetAllAsync();
        Task<ResultCommentDTO?> GetByIdAsync(int id);
        Task<List<ResultCommentDTO>> GetByAppUserIdAsync(int appUserId);

        Task<bool> CreateUserCommentAsync(CreateCommentDTO dto);
        Task<bool> CreateAdminCommentAsync(CreateCommentWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateCommentWithFileDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<List<SelectListItem>> GetBlogDropdownItemsAsync();
    }
}

