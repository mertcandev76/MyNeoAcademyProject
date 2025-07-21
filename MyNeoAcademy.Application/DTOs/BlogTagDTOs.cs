using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class BlogTagReferenceDTO
    {
        public int BlogTagID { get; set; }
        public BlogReferenceDTO? Blog { get; set; }
        public TagReferenceDTO? Tag { get; set; }
    }

    public class CreateBlogTagDTO
    {
        public int BlogID { get; set; }
        public int TagID { get; set; }
    }

    public class UpdateBlogTagDTO : CreateBlogTagDTO
    {
        public int BlogTagID { get; set; }
    }

    public class ResultBlogTagDTO:CreateBlogTagDTO
    {
        public int BlogTagID { get; set; }
        public BlogReferenceDTO Blog { get; set; } = null!;
        public TagReferenceDTO Tag { get; set; } = null!;
    }

}
