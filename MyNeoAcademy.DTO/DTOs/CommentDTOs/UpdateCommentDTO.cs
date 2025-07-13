using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CommentDTOs
{
    public class UpdateCommentDTO: CreateCommentDTO
    {
        public int CommentID { get; set; }
    }
}
