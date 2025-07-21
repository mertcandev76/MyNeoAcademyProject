using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CreateCourseDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();


            CreateMap<Category, CategoryReferenceDTO>();
            CreateMap<Instructor, InstructorReferenceDTO>();


            CreateMap<Course, Course>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor));


            CreateMap<CreateCourseWithFileDTO, Course>()
           .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); 

            CreateMap<UpdateCourseWithFileDTO, Course>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

        }
    }
}
