using AutoMapper;
using MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AboutFeatureMapping : Profile
    {
        public AboutFeatureMapping()
        {

            CreateMap<AboutFeature, CreateAboutFeatureDTO>().ReverseMap();
            CreateMap<AboutFeature, UpdateAboutFeatureDTO>().ReverseMap();
            CreateMap<AboutFeature, ResultAboutFeatureDTO>().ReverseMap();
        }
    }
}