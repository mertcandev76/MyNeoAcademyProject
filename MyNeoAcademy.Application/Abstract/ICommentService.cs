using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MyNeoAcademy.Application.Abstract
{
    public interface ICommentService : IGenericService<Comment, CreateCommentDTO, UpdateCommentDTO, ResultCommentDTO>
    {
        Task<List<ResultCommentDTO>> GetByCourseIdAsync(int courseId);
        Task<List<ResultCommentDTO>> GetAllWithIncludesAsync();
        Task<ResultCommentDTO?> GetByIdWithIncludesAsync(int id);
        Task<List<ResultCommentDTO>> GetByIdWithIncludesBlogAsync(int blogId);
        Task<PagedResultDTO<ResultCommentDTO>> GetPagedAsync(int page, int pageSize);
        Task<PagedResultDTO<ResultCommentDTO>> GetPagedByBlogAsync(int blogId, int page, int pageSize);
        Task<List<ResultCommentDTO>> GetByAppUserIdAsync(int appUserId);

        Task CreateUserCommentAsync(CreateCommentDTO dto);
        Task CreateWithFileAsync(CreateCommentWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateCommentWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}



