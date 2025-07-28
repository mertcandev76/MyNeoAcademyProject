using AutoMapper;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.DTOs;
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
    public class BlogTagManager : GenericManager<BlogTag, CreateBlogTagDTO, UpdateBlogTagDTO, ResultBlogTagDTO>, IBlogTagService
    {
        private readonly IBlogTagRepository _blogTagRepository;

        public BlogTagManager(IBlogTagRepository blogTagRepository, IMapper mapper) : base(blogTagRepository, mapper)
        {
            _blogTagRepository = blogTagRepository;
        }

        public async Task<List<ResultBlogTagDTO>> GetAllWithIncludesAsync()
        {
            var entities = await _blogTagRepository.GetAllWithIncludesAsync();
            var dtos = _mapper.Map<List<ResultBlogTagDTO>>(entities);
            return dtos;
        }

        public async Task<ResultBlogTagDTO?> GetByIdWithIncludesAsync(int id)
        {
            var entity = await _blogTagRepository.GetByIdWithIncludesAsync(id);
            var dto = _mapper.Map<ResultBlogTagDTO?>(entity);
            return dto;
        }

        public async Task<bool> ExistsAsync(int blogId, int tagId)
        {
            return await _blogTagRepository.ExistsAsync(blogId, tagId);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _blogTagRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _blogTagRepository.DeleteAsync(entity);
            return true;
        }
    }
}
