using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICommentService : IGenericService<
       Comment,
       CreateCommentDTO,
       UpdateCommentDTO,
       ResultCommentDTO
   >
    {




        Task<List<ResultCommentDTO>> GetAllWithIncludesAsync();
        Task<ResultCommentDTO?> GetByIdWithIncludesAsync(int id);
        Task<List<ResultCommentDTO>> GetByIdWithIncludesBlogAsync(int blogId);

        // Kullanıcı yorumu (resimsiz)
        Task CreateUserCommentAsync(CreateCommentDTO dto);

        // Admin yorumu(resimli)
        Task CreateWithFileAsync(CreateCommentWithFileDTO dto, string webRootPath);
        Task UpdateWithFileAsync(UpdateCommentWithFileDTO dto, string webRootPath);

        Task<bool> DeleteByIdAsync(int id);
    }
}
