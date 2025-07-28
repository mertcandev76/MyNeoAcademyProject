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
                .ForMember(dest => dest.InstructorID, opt => opt.Ignore());

            CreateMap<Instructor, UpdateInstructorDTO>().ReverseMap();

            CreateMap<Course, CourseReferenceDTO>();

            CreateMap<Instructor, ResultInstructorDTO>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));

            CreateMap<CreateInstructorWithFileDTO, Instructor>()
                .ForMember(dest => dest.InstructorID, opt => opt.Ignore()); 

            CreateMap<UpdateInstructorWithFileDTO, Instructor>();




        }
    }
}
