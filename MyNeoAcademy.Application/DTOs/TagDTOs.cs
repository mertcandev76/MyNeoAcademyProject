using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class TagReferenceDTO
    {
        public int TagID { get; set; }
        public string Name { get; set; } = null!;
    }
        public class CreateTagDTO
        {
            public string Name { get; set; } = null!;
        }
    public class ResultTagDTO : CreateTagDTO
    {
        public int TagID { get; set; }
        public List<BlogReferenceDTO> Blogs { get; set; } = new List<BlogReferenceDTO>();
    }

    public class UpdateTagDTO : CreateTagDTO
    {
        public int TagID { get; set; }
    }
}
