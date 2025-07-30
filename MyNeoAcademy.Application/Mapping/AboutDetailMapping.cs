using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping
{
    public class AboutDetailMapping:Profile
    {
        public AboutDetailMapping()
        {
            CreateMap<AboutDetail, CreateAboutDetailDTO>().ReverseMap();
            CreateMap<AboutDetail, UpdateAboutDetailDTO>().ReverseMap();
            CreateMap<AboutDetail, ResultAboutDetailDTO>().ReverseMap();
        }
    }
}
