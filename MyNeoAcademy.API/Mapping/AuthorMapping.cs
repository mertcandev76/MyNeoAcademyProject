using AutoMapper;
using MyNeoAcademy.DTO.DTOs.AuthorDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, ResultAuthorDTO>().ReverseMap();


        }
    }
}