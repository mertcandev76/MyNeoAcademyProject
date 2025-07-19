using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    }
}
