using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class BlogTagMapping : Profile
    {
        public BlogTagMapping()
        {
            // Create ve Update DTO'lar (ID ile ilişki kuruluyor)
            CreateMap<BlogTag, CreateBlogTagDTO>().ReverseMap();
            CreateMap<BlogTag, UpdateBlogTagDTO>().ReverseMap();

            // Reference DTO - detay için
            CreateMap<BlogTag, BlogTagReferenceDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));

            // Result DTO - detaylı gösterim için
            CreateMap<BlogTag, ResultBlogTagDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));

        }
    }
}
