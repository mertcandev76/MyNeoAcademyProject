using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class RecentBlogPost
    {
        public int RecentBlogPostID { get; set; } 

        public string CompactTitle { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public string? ThumbnailUrl { get; set; }
    }
}
