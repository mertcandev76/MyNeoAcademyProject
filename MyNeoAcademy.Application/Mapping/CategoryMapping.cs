using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {

            //CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            //CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            //CreateMap<Category, ResultCategoryDTO>().ReverseMap();


            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();
            CreateMap<Course, CourseReferenceDTO>();

            CreateMap<Category, ResultCategoryDTO>()
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.Blogs))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));


        }
    }
}