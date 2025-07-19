using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AboutMapping:Profile
    {
        public AboutMapping()
        {
            // Basic DTO Mappings
            CreateMap<About, CreateAboutDTO>().ReverseMap();
            CreateMap<About, UpdateAboutDTO>().ReverseMap();
            //.ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features));


            CreateMap<AboutFeature, AboutFeatureReferenceDTO>();

            CreateMap<About, ResultAboutDTO>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features));

            // File DTOs (ignore image paths)
            CreateMap<CreateAboutWithFileDTO, About>()
                .ForMember(dest => dest.ImageFrontUrl, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBackUrl, opt => opt.Ignore());

            CreateMap<UpdateAboutWithFileDTO, About>()
                .ForMember(dest => dest.ImageFrontUrl, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBackUrl, opt => opt.Ignore());
        }
    }
}
