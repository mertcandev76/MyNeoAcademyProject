using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IBlogTagRepository : IRepository<BlogTag>
    {
        Task<List<BlogTag>> GetAllWithIncludesAsync();
        Task<BlogTag?> GetByIdWithIncludesAsync(int id);

        //Veritabanında belirttiğim BlogID ve TagID ilişkisi var mı?
        Task<bool> ExistsAsync(int blogId, int tagId);
    }
}
