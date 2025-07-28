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
    public class RecentBlogPostMapping:Profile
    {
        public RecentBlogPostMapping()
        {
            CreateMap<RecentBlogPost, CreateRecentBlogPostDTO>().ReverseMap();
            CreateMap<RecentBlogPost, UpdateRecentBlogPostDTO>().ReverseMap();
            CreateMap<RecentBlogPost, ResultRecentBlogPostDTO>().ReverseMap();

            CreateMap<CreateRecentBlogPostWithFileDTO, RecentBlogPost>()
          .ForMember(dest => dest.RecentBlogPostID, opt => opt.Ignore());

            CreateMap<UpdateRecentBlogPostWithFileDTO, RecentBlogPost>();

            CreateMap<CreateRecentBlogPostDTO, RecentBlogPost>()
           .ForMember(dest => dest.PublishDate, opt => opt.Ignore());
        }
    }
}
