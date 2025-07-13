using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Category
    {

        public int CategoryID { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IconClass { get; set; }

        // İlişkiler
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
    }
}
