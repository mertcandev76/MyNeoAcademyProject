using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, CreateCourseDTO>().ReverseMap();
            CreateMap<Course, UpdateCourseDTO>().ReverseMap();
            CreateMap<Course, ResultCourseDTO>().ReverseMap();

        }
    }
}
