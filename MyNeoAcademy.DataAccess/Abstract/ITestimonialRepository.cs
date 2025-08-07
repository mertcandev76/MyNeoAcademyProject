using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface ITestimonialRepository : IRepository<Testimonial>
    {
        Task<List<Testimonial>> GetAllWithIncludesAsync();
        Task<Testimonial?> GetByIdWithIncludesAsync(int id);
        Task<Testimonial?> GetByAppUserIdAsync(int appUserId);
    }
}
