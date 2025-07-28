using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {

            CreateMap<Comment, CreateCommentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CommentID, opt => opt.Ignore());

            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();

            CreateMap<Comment, ResultCommentDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog));

            CreateMap<CreateCommentWithFileDTO, Comment>()
                .ForMember(dest => dest.CommentID, opt => opt.Ignore()); 

            CreateMap<UpdateCommentWithFileDTO, Comment>();

            CreateMap<CreateCommentDTO, Comment>()
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

        }
    }
}