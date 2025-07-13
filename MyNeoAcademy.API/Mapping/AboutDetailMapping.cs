using AutoMapper;
using MyNeoAcademy.DTO.DTOs.AboutDetailDTOs;
using MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AboutDetailMapping : Profile
    {
        public AboutDetailMapping()
        {

            CreateMap<AboutDetail, CreateAboutDetailDTO>().ReverseMap();
            CreateMap<AboutDetail, UpdateAboutDetailDTO>().ReverseMap();
            CreateMap<AboutDetail, ResultAboutDetailDTO>().ReverseMap();
        }
    }
}