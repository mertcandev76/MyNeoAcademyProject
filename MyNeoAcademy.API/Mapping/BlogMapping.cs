using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BlogMapping:Profile
    {
        public BlogMapping()
        {
            CreateMap<CreateBlogDTO, Blog>().ReverseMap();
            CreateMap<UpdateBlogDTO, Blog>().ReverseMap();
            CreateMap<ResultBlogDTO, Blog>().ReverseMap();
        }
    }
}
