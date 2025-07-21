using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IAuthorService:IGenericService<Author>
    {
        //Özel Metotlar
        Task<Author?> GetAllWithBlogAsync(int id);
    }
}
