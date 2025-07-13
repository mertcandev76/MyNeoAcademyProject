using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CommentDTOs
{
    public class ResultCommentDTO:CreateCommentDTO
    {
        public int CommentID { get; set; }
        public ResultBlogDTO? Blog { get; set; }
    }
}
