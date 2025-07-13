using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CommentDTOs
{
    public class CreateCommentDTO
    {
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int BlogID { get; set; }
    }
}
