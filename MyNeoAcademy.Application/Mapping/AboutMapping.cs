using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class AboutMapping:Profile
    {
        public AboutMapping()
        {

            CreateMap<About, CreateAboutDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AboutID, opt => opt.Ignore());


            CreateMap<About, UpdateAboutDTO>().ReverseMap();


            CreateMap<AboutFeature, AboutFeatureReferenceDTO>();


            CreateMap<About, ResultAboutDTO>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features));


            CreateMap<CreateAboutWithFileDTO, About>()
                .ForMember(dest => dest.AboutID, opt => opt.Ignore()); 

            CreateMap<UpdateAboutWithFileDTO, About>();



        }
    }
}
