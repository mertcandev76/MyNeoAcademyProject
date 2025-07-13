using AutoMapper;
using MyNeoAcademy.DTO.DTOs.CommentDTOs;
using MyNeoAcademy.Entity.Entities;

namespace MyNeoAcademy.API.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {

            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, ResultCommentDTO>().ReverseMap();
        }
    }
}