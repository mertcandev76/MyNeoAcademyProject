using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
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
