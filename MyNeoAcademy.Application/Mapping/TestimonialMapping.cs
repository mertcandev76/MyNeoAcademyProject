using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.Application.Mapping
{
    public class TestimonialMapping : Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, CreateTestimonialDTO>()
                .ReverseMap()
                .ForMember(dest => dest.TestimonialID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<Testimonial, UpdateTestimonialDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<Testimonial, ResultTestimonialDTO>()
                .ReverseMap()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<CreateTestimonialWithFileDTO, Testimonial>()
                .ForMember(dest => dest.TestimonialID, opt => opt.Ignore())
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());

            CreateMap<UpdateTestimonialWithFileDTO, Testimonial>()
                .ForMember(dest => dest.AppUser, opt => opt.Ignore());
        }
    }
}
