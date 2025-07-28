using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IRecentBlogPostService : IGenericService<
       RecentBlogPost,
       CreateRecentBlogPostDTO,
       UpdateRecentBlogPostDTO,
       ResultRecentBlogPostDTO
   >
    {
        Task CreateWithFileAsync(CreateRecentBlogPostWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateRecentBlogPostWithFileDTO dto, string webRootPath);
        Task<bool> DeleteByIdAsync(int id);
    }
}
