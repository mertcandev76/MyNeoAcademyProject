using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        [Required, StringLength(100)]
        public string CourseName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int CourseCategoryID { get; set; }
        public CourseCategory? CourseCategory { get; set; }
        public decimal Price { get; set; }
        public bool IsShown { get; set; } = false;  // default false burada verilebilir
    }
}
