using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IBlogService : IGenericService<
     Blog,
     CreateBlogDTO,
     UpdateBlogDTO,
     ResultBlogDTO>
    {
        Task<List<ResultBlogDTO>> GetAllWithIncludesAsync();
        Task<ResultBlogDTO?> GetByIdWithIncludesAsync(int id);
        Task CreateWithFileAsync(CreateBlogWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateBlogWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}
