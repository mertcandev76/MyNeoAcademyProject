using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class BlogTag
    {
        public int BlogTagID { get; set; }

        public int BlogID { get; set; }
        public Blog? Blog { get; set; }

        public int TagID { get; set; }
        public Tag? Tag { get; set; }
    }
}
