using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;



namespace MyNeoAcademy.API.Mapping
{
    public class StatisticMapping : Profile
    {
        public StatisticMapping()
        {

            CreateMap<Statistic, CreateStatisticDTO>().ReverseMap();
            CreateMap<Statistic, UpdateStatisticDTO>().ReverseMap();
            CreateMap<Statistic, ResultStatisticDTO>().ReverseMap();
        }
    }
}