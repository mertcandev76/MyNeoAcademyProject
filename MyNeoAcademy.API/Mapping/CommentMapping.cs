using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {

            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();

            CreateMap<Comment, ResultCommentDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog));


            CreateMap<CreateCommentWithFileDTO, Comment>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<UpdateCommentWithFileDTO, Comment>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}