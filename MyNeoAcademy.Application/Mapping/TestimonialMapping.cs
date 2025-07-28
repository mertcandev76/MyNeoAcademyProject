using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {

            CreateMap<Testimonial, CreateTestimonialDTO>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDTO>().ReverseMap();
            CreateMap<Testimonial, ResultTestimonialDTO>().ReverseMap();

            CreateMap<CreateTestimonialWithFileDTO, Testimonial>()
          .ForMember(dest => dest.TestimonialID, opt => opt.Ignore());

            CreateMap<UpdateTestimonialWithFileDTO, Testimonial>();

        }
    }
}
