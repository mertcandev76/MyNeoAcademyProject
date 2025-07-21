using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CommentReferenceDTO
    {
        public int CommentID { get; set; }
        public string UserName { get; set; } = null!;
        public string? Content { get; set; }
    }
    public class CreateCommentDTO
    {
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public int BlogID { get; set; }
    }
    public class ResultCommentDTO : CreateCommentDTO
    {
        public int CommentID { get; set; }
        public BlogReferenceDTO? Blog { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class UpdateCommentDTO : CreateCommentDTO
    {
        public int CommentID { get; set; }

    }
    public class CreateCommentWithFileDTO : CreateCommentDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class UpdateCommentWithFileDTO : CreateCommentWithFileDTO
    {
        public int CommentID { get; set; }
    }
}
