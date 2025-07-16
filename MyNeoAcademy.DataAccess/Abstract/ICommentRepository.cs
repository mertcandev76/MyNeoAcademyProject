using MyNeoAcademy.DTO.DTOs.CommentDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ICommentRepository:IRepository<Comment>
    {
        //Özel Metotlar
        Task<List<Comment>> GetAllWithBlogAsync();
        Task<Comment?> GetByIdWithBlogAsync(int id);
    }
}
