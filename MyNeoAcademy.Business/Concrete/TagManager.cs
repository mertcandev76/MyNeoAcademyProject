using MyNeoAcademy.Application.Abstract;
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
    public class TagManager : GenericManager<Tag>, ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagManager(ITagRepository tagRepository):base(tagRepository)  
        {
            _tagRepository = tagRepository;
        }

        public async Task<List<Tag>> GetAllWithIncludesAsync()=> await _tagRepository.GetAllWithIncludesAsync();

        public async Task<Tag?> GetByIdWithIncludesAsync(int id)=>await _tagRepository.GetByIdWithIncludesAsync(id);
    }
}
