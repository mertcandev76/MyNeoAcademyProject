using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs
{
    public class ResultBlogCategoryDTO
    {
        public int BlogCategoryID { get; set; }
        public string Name { get; set; } = null!;


        //public List<ResultBlogDTO>? Blogs { get; set; } // ilişkili bloglar
    }
}
