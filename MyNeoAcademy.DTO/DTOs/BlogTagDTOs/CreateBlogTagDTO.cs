using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogTagDTOs
{
    public class CreateBlogTagDTO
    {

        public int BlogID { get; set; }

        public int TagID { get; set; }
    }
}
