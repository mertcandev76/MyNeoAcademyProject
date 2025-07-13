using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BlogTagDTOs
{
    public class UpdateBlogTagDTO:CreateBlogTagDTO
    {
        public int BlogTagID { get; set; }
    }
}
