using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Blog
    {

        public int BlogID { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }


        public int? AuthorID { get; set; }
        public Author? Author { get; set; }

        public int? CategoryID { get; set; }
        public Category? Category { get; set; }




        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();



    }
}
