using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BlogMapping:Profile
    {
        public BlogMapping()
        {

            CreateMap<Blog, CreateBlogDTO>().ReverseMap();
            CreateMap<Blog, UpdateBlogDTO>().ReverseMap();
            CreateMap<Blog, ResultBlogDTO>().ReverseMap();


        }
    }
}
