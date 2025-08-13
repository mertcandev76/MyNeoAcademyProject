using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;




namespace MyNeoAcademy.Application.Mapping
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CreateCourseDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());

            CreateMap<Course, UpdateCourseDTO>().ReverseMap();

            CreateMap<Category, CategoryReferenceDTO>();
            CreateMap<Instructor, InstructorReferenceDTO>();

            CreateMap<Comment, CommentReferenceDTO>();
            CreateMap<CourseEnrollment, CourseEnrollmentReferenceDTO>();
            CreateMap<CourseLike, CourseLikeReferenceDTO>();

            CreateMap<Course, ResultCourseDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.CourseEnrollments))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.CourseLikes));

            CreateMap<CreateCourseWithFileDTO, Course>()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());

            CreateMap<UpdateCourseWithFileDTO, Course>();
        }
    }
}


