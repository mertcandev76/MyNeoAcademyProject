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
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialRepository
    {
        public TestimonialRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }


        public async Task<List<Testimonial>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(t => t.AppUser)
                .ToListAsync();
        }


        public async Task<Testimonial?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.TestimonialID == id);
        }


        public async Task<Testimonial?> GetByAppUserIdAsync(int appUserId)
        {
            return await Table
                .Include(t => t.AppUser)
                .FirstOrDefaultAsync(t => t.AppUserID == appUserId);
        }
    }
}
