using AutoMapper;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.Concrete
{
    public class TagManager : GenericManager<Tag, CreateTagDTO, UpdateTagDTO, ResultTagDTO>, ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagManager(ITagRepository tagRepository, IMapper mapper)
            : base(tagRepository, mapper)
        {
            _tagRepository = tagRepository;
        }



        public async Task<List<ResultTagDTO>> GetAllWithIncludesAsync()
        {
            var tagEntities = await _tagRepository.GetAllWithIncludesAsync();
            return _mapper.Map<List<ResultTagDTO>>(tagEntities);
        }

        public async Task<ResultTagDTO?> GetByIdWithIncludesAsync(int id)
        {
            var tag = await _tagRepository.GetByIdWithIncludesAsync(id);
            return _mapper.Map<ResultTagDTO>(tag);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _tagRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _tagRepository.DeleteAsync(entity);
            return true;
        }
    }
}
