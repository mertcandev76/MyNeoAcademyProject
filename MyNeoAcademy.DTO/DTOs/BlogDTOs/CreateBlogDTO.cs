using MyNeoAcademy.DTO.DTOs.AuthorDTOs;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
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
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }

        public int AuthorID { get; set; }

        public int CategoryID { get; set; }

    }
}
