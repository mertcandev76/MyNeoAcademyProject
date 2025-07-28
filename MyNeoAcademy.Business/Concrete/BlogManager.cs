using AutoMapper;
using Microsoft.AspNetCore.Http;
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

    public class BlogManager : GenericManager<Blog, CreateBlogDTO, UpdateBlogDTO, ResultBlogDTO>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogManager(
            IBlogRepository blogRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(blogRepository, mapper)
        {
            _blogRepository = blogRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ResultBlogDTO>> GetAllWithIncludesAsync()
        {
            var blogs = await _blogRepository.GetAllWithIncludesAsync();
            var dtos = _mapper.Map<List<ResultBlogDTO>>(blogs);

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }
            }

            return dtos;
        }

        public async Task<ResultBlogDTO?> GetByIdWithIncludesAsync(int id)
        {
            var blog = await _blogRepository.GetByIdWithIncludesAsync(id);
            var dto = _mapper.Map<ResultBlogDTO>(blog);

            if (dto != null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                    ? $"{request.Scheme}://{request.Host}"
                    : "https://localhost:7230";

                if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
                {
                    dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
                }
            }

            return dto;
        }


        public override async Task CreateAsync(CreateBlogDTO dto)
        {
            var entity = _mapper.Map<Blog>(dto);
            entity.PublishDate = DateTime.UtcNow;

            await _blogRepository.CreateAsync(entity);
        }

        public async Task CreateWithFileAsync(CreateBlogWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Görsel zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/blogs");

            await CreateAsync(dto);
        }


        public async Task UpdateWithFileAsync(UpdateBlogWithFileDTO dto, string webRootPath)
        {
            var entity = await _blogRepository.GetByIdAsync(dto.BlogID);
            if (entity == null)
                throw new Exception("Blog bulunamadı.");

            if (dto.ImageFile != null)
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/blogs");

            _mapper.Map(dto, entity);

            await _blogRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _blogRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _blogRepository.DeleteAsync(entity);
            return true;
        }
    }
}