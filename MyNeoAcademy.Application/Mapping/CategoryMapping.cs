using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {

            CreateMap<Category, CreateCategoryDTO>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryID, opt => opt.Ignore());

            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();
            CreateMap<Blog, CourseReferenceDTO>();

            CreateMap<Category, ResultCategoryDTO>()
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.Blogs))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));




        }
    }
}