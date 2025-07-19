using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class InstructorMapping : Profile
    {
        public InstructorMapping()
        {

            CreateMap<Instructor, CreateInstructorDTO>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDTO>().ReverseMap();


            CreateMap<Course, CourseReferenceDTO>();
            CreateMap<Instructor, ResultInstructorDTO>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));

            CreateMap<CreateInstructorWithFileDTO, Instructor>()
          .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); 

            CreateMap<UpdateInstructorWithFileDTO, Instructor>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
