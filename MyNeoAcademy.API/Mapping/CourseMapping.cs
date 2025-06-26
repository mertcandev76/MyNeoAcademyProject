using AutoMapper;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseDTO, Course>().ReverseMap();
            CreateMap<UpdateCourseDTO, Course>().ReverseMap();
            CreateMap<ResultCourseDTO, Course>().ReverseMap();
        }
    }
}
