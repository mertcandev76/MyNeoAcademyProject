using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class AboutFeatureMapping : Profile
    {
        public AboutFeatureMapping()
        {


            CreateMap<AboutFeature, CreateAboutFeatureDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AboutFeatureID, opt => opt.Ignore());


            CreateMap<AboutFeature, UpdateAboutFeatureDTO>().ReverseMap();


            CreateMap<About, AboutReferenceDTO>();


            CreateMap<AboutFeature, ResultAboutFeatureDTO>()
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About));

        }
    }
}