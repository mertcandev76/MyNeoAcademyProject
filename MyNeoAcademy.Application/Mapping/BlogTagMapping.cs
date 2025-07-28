using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class BlogTagMapping : Profile
    {
        public BlogTagMapping()
        {


            CreateMap<BlogTag, CreateBlogTagDTO>()
                .ReverseMap()
                .ForMember(dest => dest.BlogTagID, opt => opt.Ignore());


            CreateMap<BlogTag, UpdateBlogTagDTO>().ReverseMap();


            CreateMap<Blog, BlogReferenceDTO>();
            CreateMap<Blog, TagReferenceDTO>();


            CreateMap<BlogTag, ResultBlogTagDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));



        }
    }
}
