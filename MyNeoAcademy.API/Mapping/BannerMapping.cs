using AutoMapper;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class BannerMapping:Profile
    {
        public BannerMapping()
        {
            CreateMap<CreateBannerDTO, Banner>().ReverseMap();
            CreateMap<UpdateBannerDTO, Banner>().ReverseMap();
        }
    }
}
