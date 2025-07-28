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



            CreateMap<Blog, CreateBlogDTO>()
                .ReverseMap()
                .ForMember(dest => dest.BlogID, opt => opt.Ignore());

            CreateMap<Blog, UpdateBlogDTO>().ReverseMap();

            CreateMap<Category, CategoryReferenceDTO>();
            CreateMap<Author, AuthorReferenceDTO>();
            CreateMap<Comment, CommentReferenceDTO>();
            CreateMap<Tag, TagReferenceDTO>();

            CreateMap<Blog, ResultBlogDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom<BlogTagsToTagReferenceDTOResolver>());

            CreateMap<CreateBlogWithFileDTO, Blog>()
                .ForMember(dest => dest.BlogID, opt => opt.Ignore());

            CreateMap<UpdateBlogWithFileDTO, Blog>();

            CreateMap<CreateBlogDTO, Blog>()
    .ForMember(dest => dest.PublishDate, opt => opt.Ignore());

        }
    }
}
