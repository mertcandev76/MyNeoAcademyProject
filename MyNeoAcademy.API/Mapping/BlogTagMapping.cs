using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogTagDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BlogTagMapping : Profile
    {
        public BlogTagMapping()
        {

            CreateMap<BlogTag, CreateBlogTagDTO>().ReverseMap();
            CreateMap<BlogTag, UpdateBlogTagDTO>().ReverseMap();
            CreateMap<BlogTag, ResultBlogTagDTO>().ReverseMap();
        }
    }
}