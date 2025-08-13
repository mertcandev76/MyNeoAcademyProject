using AutoMapper;
using MyNeoAcademy.Application.DTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping
{
    public class CourseEnrollmentMapping : Profile
    {
        public CourseEnrollmentMapping()
        {
            CreateMap<CourseEnrollment, CourseEnrollmentReferenceDTO>();

            CreateMap<CourseEnrollment, ResultCourseEnrollmentDTO>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser));

            CreateMap<CreateCourseEnrollmentDTO, CourseEnrollment>();
        }
    }
}

