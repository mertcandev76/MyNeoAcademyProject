using AutoMapper;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class MessageMapping:Profile
    {
        public MessageMapping()
        {
            CreateMap<CreateMessageDTO, Message>().ReverseMap();
            CreateMap<UpdateMessageDTO, Message>().ReverseMap();
        }
    }
}
