using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Common;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
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
    public class UpdateAuthorDTO : CreateAuthorDTO, IHasId
    {
        public int AuthorID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => AuthorID;
            set => AuthorID = value;
        }
    }
    public class UpdateAuthorWithFileDTO : CreateAuthorWithFileDTO, IHasId
    {
        public int AuthorID { get; set; }
        public int Id
        {
            get => AuthorID;
            set => AuthorID = value;
        }
    }
}
