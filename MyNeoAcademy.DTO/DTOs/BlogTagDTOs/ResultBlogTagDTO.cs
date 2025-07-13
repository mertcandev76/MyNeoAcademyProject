using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.TagDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogTagDTOs
{
    public class ResultBlogTagDTO:CreateBlogTagDTO
    {
        public int BlogTagID { get; set; }
        public ResultBlogDTO? Blog { get; set; }
        public ResultTagDTO? Tag { get; set; }
    }
}
