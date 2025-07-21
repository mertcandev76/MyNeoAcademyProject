using AutoMapper;
using MyNeoAcademy.Application.Mapping.Resolvers;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class BlogMapping:Profile
    {
        public BlogMapping()
        {
            // Create ve Update DTO'lar
            CreateMap<Blog, CreateBlogDTO>().ReverseMap();
            CreateMap<Blog, UpdateBlogDTO>().ReverseMap();

            // Referans DTO'lar
            CreateMap<Category, CategoryReferenceDTO>();
            CreateMap<Author, AuthorReferenceDTO>();
            CreateMap<Comment, CommentReferenceDTO>();
            CreateMap<Tag, TagReferenceDTO>();

            // Result DTO - Detay için
            CreateMap<Blog, ResultBlogDTO>()
                .ForMember(dest => dest.BlogID, opt => opt.MapFrom(src => src.BlogID))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom<BlogTagsToTagReferenceDTOResolver>());


            CreateMap<CreateBlogWithFileDTO, Blog>()
    .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<UpdateBlogWithFileDTO, Blog>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
