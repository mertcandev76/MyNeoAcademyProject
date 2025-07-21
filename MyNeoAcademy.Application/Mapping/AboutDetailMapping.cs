using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
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