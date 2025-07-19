using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class AboutManager : GenericManager<About>, IAboutService
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutManager(IAboutRepository aboutRepository):base(aboutRepository) 
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<About?> GetAllWithAboutFeatureAsync(int id)=> await _aboutRepository.GetAllWithAboutFeatureAsync(id);
    }
}
