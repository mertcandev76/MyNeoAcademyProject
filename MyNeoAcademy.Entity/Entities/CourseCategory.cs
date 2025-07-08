using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class CourseCategory
    {
        public int CourseCategoryID { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public bool IsShown { get; set; } = false;  // default false burada verilebilir
        public ICollection<Course>? Courses { get; set; }
    }
}
