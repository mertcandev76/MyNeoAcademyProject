using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Instructor
    {
        public int InstructorID { get; set; }

        public string FullName { get; set; } = null!;

        public string? Title { get; set; }

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public string? FacebookUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? WebsiteUrl { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
