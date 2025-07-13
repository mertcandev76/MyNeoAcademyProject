using AutoMapper;
using MyNeoAcademy.DTO.DTOs.TagDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class TagMapping : Profile
    {
        public TagMapping()
        {

            CreateMap<Tag, CreateTagDTO>().ReverseMap();
            CreateMap<Tag, UpdateTagDTO>().ReverseMap();
            CreateMap<Tag, ResultTagDTO>().ReverseMap();
        }
    }
}
