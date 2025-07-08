using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class ResultCourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CourseCategoryID { get; set; }
        public decimal Price { get; set; }
        public bool IsShown { get; set; }

        public ResultCourseCategoryDTO? CourseCategory { get; set; }

    }
}
