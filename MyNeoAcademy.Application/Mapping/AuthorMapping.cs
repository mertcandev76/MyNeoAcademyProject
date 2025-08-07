using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System.Runtime;

namespace MyNeoAcademy.Application.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {

            CreateMap<Author, CreateAuthorDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AuthorID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore()); 


            CreateMap<Author, UpdateAuthorDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<CreateAuthorWithFileDTO, Author>()
                .ForMember(dest => dest.AuthorID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<UpdateAuthorWithFileDTO, Author>()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());


            CreateMap<Blog, BlogReferenceDTO>();


            CreateMap<Author, ResultAuthorDTO>()
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.Blogs))
                .ForMember(dest => dest.AppUserFullName,
                    opt => opt.MapFrom(src =>
                        src.AppUser != null
                            ? $"{src.AppUser.FirstName} {src.AppUser.LastName}"
                            : null));
        }
    }
}