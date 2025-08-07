using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class InstructorMapping : Profile
    {
        public InstructorMapping()
        {

            CreateMap<Instructor, CreateInstructorDTO>()
                .ReverseMap()
                .ForMember(dest => dest.InstructorID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore()); 


            CreateMap<Instructor, UpdateInstructorDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<CreateInstructorWithFileDTO, Instructor>()
                .ForMember(dest => dest.InstructorID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<UpdateInstructorWithFileDTO, Instructor>()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<Course, CourseReferenceDTO>();


            CreateMap<Instructor, ResultInstructorDTO>()
     .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
     .ForMember(dest => dest.AppUserFullName,
         opt => opt.MapFrom(src =>
             src.AppUser != null
                 ? $"{src.AppUser.FirstName} {src.AppUser.LastName}"
                 : null));

        }
    }
}
