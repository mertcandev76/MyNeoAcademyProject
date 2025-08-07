using AutoMapper;
using MyNeoAcademy.Application.DTOs.Role;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Mapping.Role
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<AppRole, ResultRoleDTO>().ReverseMap();


            CreateMap<CreateRoleDTO, AppRole>();
            CreateMap<UpdateRoleDTO, AppRole>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
