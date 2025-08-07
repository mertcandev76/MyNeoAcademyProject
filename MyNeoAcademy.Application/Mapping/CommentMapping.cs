using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
     public class CommentMapping : Profile
    {
        public CommentMapping()
        {

            CreateMap<Blog, BlogReferenceDTO>();


            CreateMap<Comment, CreateCommentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CommentID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<Comment, UpdateCommentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<Comment, ResultCommentDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog));



            CreateMap<CreateCommentWithFileDTO, Comment>()
                .ForMember(dest => dest.CommentID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<UpdateCommentWithFileDTO, Comment>()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<CreateCommentDTO, Comment>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());
        }
    }
}