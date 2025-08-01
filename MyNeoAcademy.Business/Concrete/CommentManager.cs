﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
using System.Xml.Linq;

namespace MyNeoAcademy.Business.Concrete
{
    public class CommentManager : GenericManager<Comment, CreateCommentDTO, UpdateCommentDTO, ResultCommentDTO>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentManager(
            ICommentRepository commentRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ResultCommentDTO>> GetAllWithIncludesAsync()
        {
            var comments = await _commentRepository.GetAllWithIncludesAsync();
            return MapWithImageUrls(comments);
        }

        public async Task<ResultCommentDTO?> GetByIdWithIncludesAsync(int id)
        {
            var comment = await _commentRepository.GetByIdWithIncludesAsync(id);
            if (comment == null)
                return null;

            var dto = _mapper.Map<ResultCommentDTO>(comment);
            ApplyImageUrl(dto);
            return dto;
        }

        public async Task<List<ResultCommentDTO>> GetByIdWithIncludesBlogAsync(int blogId)
        {
            var comments = await _commentRepository.GetByIdWithIncludesBlogAsync(blogId);
            return MapWithImageUrls(comments);
        }

        public async Task<PagedResultDTO<ResultCommentDTO>> GetPagedAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var comments = await _commentRepository.GetPagedCommentsAsync(skip, pageSize);
            var totalCount = await _commentRepository.GetTotalCountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var dtos = _mapper.Map<List<ResultCommentDTO>>(comments);
            foreach (var dto in dtos)
            {
                ApplyImageUrl(dto);
            }

            return new PagedResultDTO<ResultCommentDTO>
            {
                Items = dtos,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        // Blog bazlı sayfalama metodu
        public async Task<PagedResultDTO<ResultCommentDTO>> GetPagedByBlogAsync(int blogId, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var query = _commentRepository.Table.Where(c => c.BlogID == blogId);

            var totalCount = await query.CountAsync();

            var comments = await query
                .OrderByDescending(c => c.CreatedDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var dtos = _mapper.Map<List<ResultCommentDTO>>(comments);
            foreach (var dto in dtos)
            {
                ApplyImageUrl(dto);
            }

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResultDTO<ResultCommentDTO>
            {
                Items = dtos,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        public override async Task CreateAsync(CreateCommentDTO dto)
        {
            var entity = _mapper.Map<Comment>(dto);
            entity.CreatedDate = DateTime.UtcNow;
            await _commentRepository.CreateAsync(entity);
        }

        public async Task CreateUserCommentAsync(CreateCommentDTO dto)
        {
            await CreateAsync(dto);
        }

        public async Task CreateWithFileAsync(CreateCommentWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile != null)
            {
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/comments");
            }

            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateCommentWithFileDTO dto, string webRootPath)
        {
            var entity = await _commentRepository.GetByIdAsync(dto.CommentID);
            if (entity == null)
                throw new Exception("Yorum bulunamadı.");

            if (dto.ImageFile != null)
            {
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/comments");
            }

            _mapper.Map(dto, entity);
            await _commentRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _commentRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _commentRepository.DeleteAsync(entity);
            return true;
        }

        private string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";
        }

        private void ApplyImageUrl(ResultCommentDTO dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
            {
                dto.ImageUrl = $"{GetBaseUrl()}/{dto.ImageUrl.TrimStart('/')}";
            }
        }

        private List<ResultCommentDTO> MapWithImageUrls(List<Comment> comments)
        {
            var dtos = _mapper.Map<List<ResultCommentDTO>>(comments);
            foreach (var dto in dtos)
            {
                ApplyImageUrl(dto);
            }
            return dtos;
        }
    }


}
