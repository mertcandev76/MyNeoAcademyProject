using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Abstract
{
    public interface IBlogService:IGenericService<Blog>
    {
        //Özel Metotlar
        Task<List<Blog>> GetAllWithBlogCategoryAsync();
        Task<Blog?> GetByIdWithBlogCategoryAsync(int id);
    }
}
