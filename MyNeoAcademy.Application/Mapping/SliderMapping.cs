using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class SliderMapping : Profile
    {
        public SliderMapping()
        {

            CreateMap<Slider, CreateSliderDTO>().ReverseMap();
            CreateMap<Slider, UpdateSliderDTO>().ReverseMap();
            CreateMap<Slider, ResultSliderDTO>().ReverseMap();

            CreateMap<CreateSliderWithFileDTO, Slider>()
                .ForMember(dest => dest.SliderID, opt => opt.Ignore()); 

            CreateMap<UpdateSliderWithFileDTO, Slider>();
 
        }
    }
}