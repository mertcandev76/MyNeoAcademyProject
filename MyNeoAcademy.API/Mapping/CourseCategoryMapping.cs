using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CourseCategoryMapping:Profile
    {
        public CourseCategoryMapping()
        {
            CreateMap<CourseCategory, CreateCourseCategoryDTO>().ReverseMap();
            CreateMap<CourseCategory, UpdateCourseCategoryDTO>().ReverseMap();
            CreateMap<CourseCategory, ResultCourseCategoryDTO>().ReverseMap();

        }
    }
}
