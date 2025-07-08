using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BlogCategoryMapping:Profile
    {
        public BlogCategoryMapping()
        {
            CreateMap<BlogCategory, CreateBlogCategoryDTO>().ReverseMap();
            CreateMap<BlogCategory, UpdateBlogCategoryDTO>().ReverseMap();
            CreateMap<BlogCategory, ResultBlogCategoryDTO>().ReverseMap();
        }
    }
}
