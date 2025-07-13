using MyNeoAcademy.DTO.DTOs.AuthorDTOs;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogDTOs
{
    public class ResultBlogDTO:CreateBlogDTO
    {
        public int BlogID { get; set; }

        public ResultAuthorDTO? Author { get; set; }
        public ResultCategoryDTO? Category { get; set; }

    }
}
