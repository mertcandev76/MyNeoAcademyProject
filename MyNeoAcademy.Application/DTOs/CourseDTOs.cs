using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CourseReferenceDTO
    {
        public int CourseID { get; set; }
        public string? Title { get; set; }
    }
    public class CreateCourseDTO
    {

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int Rating { get; set; }
        public int ReviewCount { get; set; }

        public int StudentCount { get; set; }

        public int LikeCount { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryID { get; set; }

        public int? InstructorID { get; set; }


    }
    public class CreateCourseWithFileDTO : CreateCourseDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class ResultCourseDTO : CreateCourseDTO
    {
        public int CourseID { get; set; }

        public CategoryReferenceDTO? Category { get; set; }

        public InstructorReferenceDTO? Instructor { get; set; } // Navigasyon property
    }
    public class UpdateCourseDTO : CreateCourseDTO
    {
        public int CourseID { get; set; }
    }
    public class UpdateCourseWithFileDTO : CreateCourseWithFileDTO
    {
        public int CourseID { get; set; }
    }
}
