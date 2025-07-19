using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {

        //Özel Metotlar
        Task<List<Category>> GetAllWithBlogAsync();
        Task<Category?> GetByIdWithBlogAsync(int id);
    }
}
