using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.DTO.DTOs.CommentDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<Comment>> GetAllWithBlogAsync()
        {
            return await Table
                .Include(c => c.Blog)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdWithBlogAsync(int id)
        {
            return await Table
               .Include(c => c.Blog)
               .FirstOrDefaultAsync(c=>c.CommentID==id);
        }
    }
}
