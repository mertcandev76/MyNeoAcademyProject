using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class NewsletterMapping : Profile
    {
        public NewsletterMapping()
        {
            CreateMap<Newsletter, CreateNewsletterDTO>().ReverseMap();
            CreateMap<Newsletter, UpdateNewsletterDTO>().ReverseMap();
            CreateMap<Newsletter, ResultNewsletterDTO>().ReverseMap();
        }
    }
}
