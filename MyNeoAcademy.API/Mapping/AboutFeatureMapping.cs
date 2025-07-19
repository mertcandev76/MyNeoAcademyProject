using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AboutFeatureMapping : Profile
    {
        public AboutFeatureMapping()
        {

            CreateMap<AboutFeature, CreateAboutFeatureDTO>().ReverseMap();
            CreateMap<AboutFeature, UpdateAboutFeatureDTO>().ReverseMap();

            CreateMap<About, AboutReferenceDTO>();

            CreateMap<AboutFeature, ResultAboutFeatureDTO>()
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.About));
        }
    }
}