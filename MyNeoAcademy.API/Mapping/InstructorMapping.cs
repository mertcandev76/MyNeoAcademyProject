using AutoMapper;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
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
        }
    }
}
