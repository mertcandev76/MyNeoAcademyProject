using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs
{
    public class CreateBlogCategoryDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

    }
}
