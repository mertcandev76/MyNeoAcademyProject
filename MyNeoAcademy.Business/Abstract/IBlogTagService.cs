using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Abstract
{
    public interface IBlogTagService : IGenericService<BlogTag>
    {
        //Özel Metotlar
        Task<List<BlogTag>> GetAllWithBlogAndTagAsync();
        Task<BlogTag?> GetByIdWithBlogAndTagAsync(int id);
    }
}
