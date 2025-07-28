using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {

            CreateMap<Course, CreateCourseDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());

            CreateMap<Course, UpdateCourseDTO>().ReverseMap();

            CreateMap<Category, CategoryReferenceDTO>();
            CreateMap<Instructor, InstructorReferenceDTO>();

            CreateMap<Course, ResultCourseDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor));

            CreateMap<CreateCourseWithFileDTO, Course>()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());

            CreateMap<UpdateCourseWithFileDTO, Course>();





        }
    }
}
