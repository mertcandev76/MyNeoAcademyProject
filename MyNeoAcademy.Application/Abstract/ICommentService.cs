using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        //Özel Metotlar
        Task<List<Comment>> GetAllWithBlogAsync();
        Task<Comment?> GetByIdWithBlogAsync(int id);
        Task<List<Comment>> GetAllByBlogIdAsync(int blogId);
    }
}
