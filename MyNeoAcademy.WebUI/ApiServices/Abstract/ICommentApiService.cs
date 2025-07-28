using Microsoft.AspNetCore.Mvc.Rendering;
using MyNeoAcademy.Application.DTOs;

namespace MyNeoAcademy.WebUI.ApiServices.Abstract
{
    public interface ICommentApiService
    {

        Task<List<ResultCommentDTO>> GetAllAsync();
        Task<ResultCommentDTO?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCommentWithFileDTO dto);
        Task<bool> UpdateAsync(UpdateCommentWithFileDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<List<SelectListItem>> GetBlogDropdownItemsAsync();
    }
}
