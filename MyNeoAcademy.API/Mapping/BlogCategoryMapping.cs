using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BlogCategoryMapping:Profile
    {
        public BlogCategoryMapping()
        {
            CreateMap<CreateBlogCategoryDTO, BlogCategory>().ReverseMap();
            CreateMap<UpdateBlogCategoryDTO, BlogCategory>().ReverseMap();
            CreateMap<ResultBlogCategoryDTO, BlogCategory>().ReverseMap();
        }
    }
}
