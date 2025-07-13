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

        public int CourseID { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Rating { get; set; } 
        public int ReviewCount { get; set; }

        public int StudentCount { get; set; }

        public int LikeCount { get; set; }

        public decimal? Price { get; set; } 

        public int CategoryID { get; set; }

        public int? InstructorID { get; set; }      


    }
}
