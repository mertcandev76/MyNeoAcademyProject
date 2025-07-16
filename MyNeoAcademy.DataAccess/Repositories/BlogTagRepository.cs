using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.DTO.DTOs.BlogTagDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Repositories
{
    public class BlogTagRepository : GenericRepository<BlogTag>, IBlogTagRepository
    {
        public BlogTagRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<BlogTag>> GetAllWithBlogAndTagAsync()
        {
            return await Table
                .Include(bT => bT.Blog)
                .Include(bT=>bT.Tag)
                .ToListAsync();
        }

        public async Task<BlogTag?> GetByIdWithBlogAndTagAsync(int id)
        {
            return await Table
            .Include(bT => bT.Blog)
            .Include(bT => bT.Tag)
           .FirstOrDefaultAsync(bT=>bT.BlogTagID==id);
        }
    }
}
