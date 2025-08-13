using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Entity.Common;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public InstructorReferenceDTO? Instructor { get; set; }
        public List<CommentReferenceDTO> Comments { get; set; } = new List<CommentReferenceDTO>();
        public List<CourseEnrollmentReferenceDTO> Enrollments { get; set; } = new List<CourseEnrollmentReferenceDTO>();
        public List<CourseLikeReferenceDTO> Likes { get; set; } = new List<CourseLikeReferenceDTO>();

        // Sistemin hesapladığı alanlar burada kalsın:
        public int Rating { get; set; }
        public int ReviewCount { get; set; }
        public int StudentCount { get; set; }
        public int LikeCount { get; set; }
    }

    public class UpdateCourseDTO : CreateCourseDTO, IHasId
    {
        public int CourseID { get; set; }

        [JsonIgnore]
        public int Id
        {
            get => CourseID;
            set => CourseID = value;
        }
    }

    public class UpdateCourseWithFileDTO : CreateCourseWithFileDTO, IHasId
    {
        public int CourseID { get; set; }

        public int Id
        {
            get => CourseID;
            set => CourseID = value;
        }
    }
}



