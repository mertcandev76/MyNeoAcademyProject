using AutoMapper;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AboutMapping:Profile
    {
        public AboutMapping()
        {

            CreateMap<About, CreateAboutDTO>().ReverseMap();
            CreateMap<About, UpdateAboutDTO>().ReverseMap();
            CreateMap<About, ResultAboutDTO>().ReverseMap();
        }
    }
}
