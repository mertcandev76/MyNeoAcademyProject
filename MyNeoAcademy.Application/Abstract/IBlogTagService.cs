using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IBlogTagService:IGenericService<BlogTag>
    {
        Task<List<BlogTag>> GetAllWithIncludesAsync();
        Task<BlogTag?> GetByIdWithIncludesAsync(int id);
        Task<bool> ExistsAsync(int blogId, int tagId);
    }
}
