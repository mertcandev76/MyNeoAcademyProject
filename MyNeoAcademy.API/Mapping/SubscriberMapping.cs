using AutoMapper;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class SubscriberMapping:Profile
    {
        public SubscriberMapping()
        {
            CreateMap<CreateSubscriberDTO, Subscriber>().ReverseMap();
            CreateMap<UpdateSubscriberDTO, Subscriber>().ReverseMap();
        }
    }
}
