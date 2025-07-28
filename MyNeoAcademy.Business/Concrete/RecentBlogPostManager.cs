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
    public class RecentBlogPostManager : GenericManager<
       RecentBlogPost,
       CreateRecentBlogPostDTO,
       UpdateRecentBlogPostDTO,
       ResultRecentBlogPostDTO>, IRecentBlogPostService
    {
        private readonly IRepository<RecentBlogPost> _recentBlogPostRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecentBlogPostManager(
            IRepository<RecentBlogPost> recentBlogPostRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(recentBlogPostRepository, mapper)
        {
            _recentBlogPostRepository = recentBlogPostRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task CreateAsync(CreateRecentBlogPostDTO dto)
        {
            var entity = _mapper.Map<RecentBlogPost>(dto);
            entity.PublishDate = DateTime.UtcNow;

            await _recentBlogPostRepository.CreateAsync(entity);
        }
        public async Task CreateWithFileAsync(CreateRecentBlogPostWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Görsel dosyası zorunludur.");

            dto.ThumbnailUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/recentblogposts");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateRecentBlogPostWithFileDTO dto, string webRootPath)
        {
            var entity = await _recentBlogPostRepository.GetByIdAsync(dto.RecentBlogPostID);
            if (entity == null)
                throw new Exception("Blog kaydı bulunamadı.");

            if (dto.ImageFile != null)
            {
                dto.ThumbnailUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/recentblogposts");
            }

            _mapper.Map(dto, entity);
            await _recentBlogPostRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _recentBlogPostRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _recentBlogPostRepository.DeleteAsync(entity);
            return true;
        }

        public override async Task<List<ResultRecentBlogPostDTO>> GetListAsync()
        {
            var list = await _recentBlogPostRepository.GetListAsync();
            var dtos = _mapper.Map<List<ResultRecentBlogPostDTO>>(list);

            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrWhiteSpace(dto.ThumbnailUrl) && !dto.ThumbnailUrl.StartsWith("http"))
                {
                    dto.ThumbnailUrl = $"{baseUrl}/{dto.ThumbnailUrl.TrimStart('/')}";
                }
            }

            return dtos;
        }

        public override async Task<ResultRecentBlogPostDTO?> GetByIdAsync(int id)
        {
            var entity = await _recentBlogPostRepository.GetByIdAsync(id);
            var dto = _mapper.Map<ResultRecentBlogPostDTO>(entity);

            if (dto != null)
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                    ? $"{request.Scheme}://{request.Host}"
                    : "https://localhost:7230";

                if (!string.IsNullOrWhiteSpace(dto.ThumbnailUrl) && !dto.ThumbnailUrl.StartsWith("http"))
                {
                    dto.ThumbnailUrl = $"{baseUrl}/{dto.ThumbnailUrl.TrimStart('/')}";
                }
            }

            return dto;
        }
    }
}
