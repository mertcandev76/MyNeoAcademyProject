using AutoMapper;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {
            CreateMap<CreateTestimonialDTO, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDTO, Testimonial>().ReverseMap();
        }
    }
}
