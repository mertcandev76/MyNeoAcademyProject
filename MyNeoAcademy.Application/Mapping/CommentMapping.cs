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
            CreateMap<Course, CourseReferenceDTO>(); 

            CreateMap<Comment, CommentReferenceDTO>();

            CreateMap<Comment, CreateCommentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CommentID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<Comment, UpdateCommentDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<Comment, ResultCommentDTO>()
                .ForMember(dest => dest.Blog, opt => opt.MapFrom(src => src.Blog))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course));

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

