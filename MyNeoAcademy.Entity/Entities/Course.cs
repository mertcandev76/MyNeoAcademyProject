using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryID { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public decimal Price { get; set; }
        public bool IsShown { get; set; }
    }
}
