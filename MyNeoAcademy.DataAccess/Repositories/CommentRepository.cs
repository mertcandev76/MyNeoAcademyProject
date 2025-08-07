using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
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
        public CommentRepository(MyNeoAcademyContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(c => c.Blog)
                .Include(c => c.AppUser) 
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                .Include(c => c.Blog)
                .Include(c => c.AppUser) 
                .FirstOrDefaultAsync(c => c.CommentID == id);
        }

        public async Task<List<Comment>> GetByIdWithIncludesBlogAsync(int blogId)
        {
            return await Table
                .Where(c => c.BlogID == blogId)
                .Include(c => c.AppUser) 
                .ToListAsync();
        }

        public async Task<List<Comment>> GetPagedCommentsAsync(int skip, int take)
        {
            return await Table
                .Include(c => c.Blog)
                .Include(c => c.AppUser) 
                .OrderByDescending(c => c.CreatedDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await Table.CountAsync();
        }


        public async Task<List<Comment>> GetByAppUserIdAsync(int appUserId)
        {
            return await Table
                .Where(c => c.AppUserID == appUserId)
                .Include(c => c.Blog)
                .Include(c => c.AppUser)
                .ToListAsync();
        }
    }

}
