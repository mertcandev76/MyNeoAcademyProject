using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Abstract
{
    public interface IAboutFeatureRepository : IRepository<AboutFeature>
    {
        Task<List<AboutFeature>> GetAllWithIncludesAsync();
        Task<AboutFeature?> GetByIdWithIncludesAsync(int id);
    }
}
