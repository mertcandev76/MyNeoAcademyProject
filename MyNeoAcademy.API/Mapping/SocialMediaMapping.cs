using AutoMapper;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class SocialMediaMapping:Profile
    {
        public SocialMediaMapping()
        {
            CreateMap<CreateSocialMediaDTO, SocialMedia>().ReverseMap();
            CreateMap<UpdateSocialMediaDTO, SocialMedia>().ReverseMap();
        }
    }
}
