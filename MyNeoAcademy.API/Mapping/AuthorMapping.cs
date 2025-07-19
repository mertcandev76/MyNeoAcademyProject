using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class AuthorMapping : Profile
    {
        public AuthorMapping()
        {

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();

            CreateMap<Blog, BlogReferenceDTO>();

            CreateMap<Author, ResultAuthorDTO>()
                .ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.Blogs));


            CreateMap<CreateAuthorWithFileDTO, Author>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<UpdateAuthorWithFileDTO, Author>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

        }
    }
}