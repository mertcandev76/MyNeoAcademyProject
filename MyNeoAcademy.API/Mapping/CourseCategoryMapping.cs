using AutoMapper;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CourseCategoryMapping:Profile
    {
        public CourseCategoryMapping()
        {
            CreateMap<CreateCourseCategoryDTO, CourseCategory>().ReverseMap();
            CreateMap<UpdateCourseCategoryDTO, CourseCategory>().ReverseMap();
            CreateMap<ResultCourseCategoryDTO, CourseCategory>().ReverseMap();

        }
    }
}
