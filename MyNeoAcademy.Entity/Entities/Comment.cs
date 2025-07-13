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
        public DateTime CreatedDate { get; set; }

        public int BlogID { get; set; }
        public Blog? Blog { get; set; }
    }
}
