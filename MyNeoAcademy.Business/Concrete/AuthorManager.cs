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
    public class AuthorManager : GenericManager<Author, CreateAuthorDTO, UpdateAuthorDTO, ResultAuthorDTO>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorManager(
            IAuthorRepository authorRepository,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor
        ) : base(authorRepository, mapper)
        {
            _authorRepository = authorRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ResultAuthorDTO>> GetAllWithIncludesAsync()
        {
            var authors = await _authorRepository.GetAllWithIncludesAsync();
            var dtos = _mapper.Map<List<ResultAuthorDTO>>(authors);

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

        public async Task<ResultAuthorDTO?> GetByIdWithIncludesAsync(int id)
        {
            var author = await _authorRepository.GetByIdWithIncludesAsync(id);
            if (author == null)
                return null;

            var dto = _mapper.Map<ResultAuthorDTO>(author);

            var request = _httpContextAccessor.HttpContext?.Request;
            string baseUrl = request != null && !string.IsNullOrEmpty(request.Host.Value)
                ? $"{request.Scheme}://{request.Host}"
                : "https://localhost:7230";

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl) && !dto.ImageUrl.StartsWith("http"))
            {
                dto.ImageUrl = $"{baseUrl}/{dto.ImageUrl.TrimStart('/')}";
            }

            return dto;
        }

        public async Task CreateWithFileAsync(CreateAuthorWithFileDTO dto, string webRootPath)
        {
            if (dto.ImageFile == null)
                throw new ArgumentException("Yazar görseli zorunludur.");

            dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/authors");
            await CreateAsync(dto);
        }

        public async Task UpdateWithFileAsync(UpdateAuthorWithFileDTO dto, string webRootPath)
        {
            var entity = await _authorRepository.GetByIdAsync(dto.AuthorID);
            if (entity == null)
                throw new Exception("Yazar bulunamadı.");

            if (dto.ImageFile != null)
            {
                dto.ImageUrl = await _fileService.SaveFileAsync(dto.ImageFile, webRootPath, "img/authors");
            }

            _mapper.Map(dto, entity);
            await _authorRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _authorRepository.DeleteAsync(entity);
            return true;
        }
    }
}
