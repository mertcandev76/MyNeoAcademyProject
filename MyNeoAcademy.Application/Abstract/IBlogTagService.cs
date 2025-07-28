using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IBlogTagService : IGenericService<
         BlogTag,
         CreateBlogTagDTO,
         UpdateBlogTagDTO,
         ResultBlogTagDTO>
    {
        Task<List<ResultBlogTagDTO>> GetAllWithIncludesAsync();
        Task<ResultBlogTagDTO?> GetByIdWithIncludesAsync(int id);
        Task<bool> ExistsAsync(int blogId, int tagId);
        Task<bool> DeleteByIdAsync(int id);
    }
}
