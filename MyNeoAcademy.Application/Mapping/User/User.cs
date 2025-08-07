using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.Application.Mapping.Resolvers;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping.User
{
    public class AppUserMappingProfile : Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<AppUser, ResultAppUserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom<AppUserProfileImageResolver>())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();
        }
    }
}
