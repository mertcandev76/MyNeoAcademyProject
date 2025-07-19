using AutoMapper;
using MyNeoAcademy.DTO.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class SliderMapping : Profile
    {
        public SliderMapping()
        {

            CreateMap<Slider, CreateSliderDTO>().ReverseMap();
            CreateMap<Slider, UpdateSliderDTO>().ReverseMap();
            CreateMap<Slider, ResultSliderDTO>().ReverseMap();

            CreateMap<CreateSliderWithFileDTO, Slider>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); // çünkü dosyayı SaveFileAsync ile yüklüyorsun

            CreateMap<UpdateSliderWithFileDTO, Slider>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}