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
    public class AboutFeatureRepository : GenericRepository<AboutFeature>, IAboutFeatureRepository
    {
        public AboutFeatureRepository(MyNeoAcademyContext myNeoAcademyContext) : base(myNeoAcademyContext)
        {
        }

        public async Task<List<AboutFeature>> GetAllWithIncludesAsync()
        {
            return await Table
                .Include(aF=>aF.About)
                .ToListAsync();
        }

        public async Task<AboutFeature?> GetByIdWithIncludesAsync(int id)
        {
            return await Table
               .Include(aF => aF.About)
               .FirstOrDefaultAsync(aF=>aF.AboutFeatureID==id);
        }
    }
}
