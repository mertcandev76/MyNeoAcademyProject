using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class CreateCourseDTO
    {

        [Required, StringLength(100)]
        public string CourseName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CourseCategoryID { get; set; }
        public decimal Price { get; set; }
        public bool IsShown { get; set; } = false;  // default false burada verilebilir

    }
}
