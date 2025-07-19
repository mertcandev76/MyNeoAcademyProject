using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
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