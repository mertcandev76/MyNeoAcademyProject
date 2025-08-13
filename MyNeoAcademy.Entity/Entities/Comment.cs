using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string UserName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Content { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int? BlogID { get; set; }
        public Blog? Blog { get; set; }

        public int? CourseID { get; set; }
        public Course? Course { get; set; }

        public int? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }

        public int Rating { get; set; }
    }
}
