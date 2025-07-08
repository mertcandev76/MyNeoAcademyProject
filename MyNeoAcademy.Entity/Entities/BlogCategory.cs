using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class BlogCategory
    {
        public int BlogCategoryID { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;
        public ICollection<Blog>? Blogs { get; set; }

    }
}
