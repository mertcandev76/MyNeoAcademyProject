using AutoMapper;
using MyNeoAcademy.Application.Mapping.Resolvers;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class TagMapping : Profile
    {
        public TagMapping()
        {

            CreateMap<Tag, CreateTagDTO>().ReverseMap();
            CreateMap<Tag, UpdateTagDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();

            // Result DTO - Detay için
            CreateMap<Tag, ResultTagDTO>()
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom<BlogTagsToBlogReferenceDTOResolver>());
        }
    }
}
