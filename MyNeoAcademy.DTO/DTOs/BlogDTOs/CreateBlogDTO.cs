using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogDTOs
{
    public class CreateBlogDTO
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public DateTime BlogDate { get; set; }

        [Required]
        public int BlogCategoryID { get; set; }

    }
}
