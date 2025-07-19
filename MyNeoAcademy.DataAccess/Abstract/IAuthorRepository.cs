using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IAuthorRepository : IRepository<Author>
    {
        //Özel Metotlar
        Task<Author?> GetAllWithBlogAsync(int id);
    }
}
