using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs
{
    public class AuthorReferenceDTO
    {
        public int AuthorID { get; set; }
        public string Name { get; set; } = null!;
    }
    public class CreateAuthorDTO
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? WebsiteUrl { get; set; }
    }
    public class CreateAuthorWithFileDTO : CreateAuthorDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class ResultAuthorDTO : CreateAuthorDTO
    {
        public int AuthorID { get; set; }

        public List<BlogReferenceDTO> Blogs { get; set; } = new List<BlogReferenceDTO>();


    }
    public class UpdateAuthorDTO : CreateAuthorDTO
    {
        public int AuthorID { get; set; }
    }
    public class UpdateAuthorWithFileDTO : CreateAuthorWithFileDTO
    {
        public int AuthorID { get; set; }
    }
}
