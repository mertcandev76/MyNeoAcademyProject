using AutoMapper;
using MyNeoAcademy.Application.DTOs.Auth;
using MyNeoAcademy.Application.Mapping.Resolvers;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping.Auth
{
    public class AuthMapping : Profile
    {
        public AuthMapping()
        {

            CreateMap<RegisterDTO, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.Ignore()) 
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Instructor, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Testimonials, opt => opt.Ignore());


            CreateMap<AppUser, TokenResultDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom<AppUserTokenImageResolver>()) 
                .ForMember(dest => dest.Roles, opt => opt.Ignore()) 
                .ForMember(dest => dest.Expiration, opt => opt.Ignore()) 
                .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
