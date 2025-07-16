using AutoMapper;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class InstructorMapping : Profile
    {
        public InstructorMapping()
        {

            CreateMap<Instructor, CreateInstructorDTO>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDTO>().ReverseMap();
            CreateMap<Instructor, ResultInstructorDTO>().ReverseMap();

            CreateMap<CreateInstructorWithFileDTO, Instructor>()
          .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); 

            CreateMap<UpdateInstructorWithFileDTO, Instructor>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
