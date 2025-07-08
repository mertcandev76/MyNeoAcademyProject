using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogDTOs
{
    public class UpdateBlogDTO:CreateBlogDTO
    {
        public int BlogID { get; set; }

    }
}
