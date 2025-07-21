using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;



namespace MyNeoAcademy.Application.Mapping
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