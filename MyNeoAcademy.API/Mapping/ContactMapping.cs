using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class ContactMapping:Profile
    {
        public ContactMapping()
        {

            CreateMap<Contact, CreateContactDTO>().ReverseMap();
            CreateMap<Contact, UpdateContactDTO>().ReverseMap();
            CreateMap<Contact, ResultContactDTO>().ReverseMap();
        }
    }
}
