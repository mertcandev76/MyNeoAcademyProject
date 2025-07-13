using AutoMapper;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {

            CreateMap<Testimonial, CreateTestimonialDTO>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDTO>().ReverseMap();
            CreateMap<Testimonial, ResultTestimonialDTO>().ReverseMap();
        }
    }
}
