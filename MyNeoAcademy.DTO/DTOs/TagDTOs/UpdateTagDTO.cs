using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.TagDTOs
{
    public class UpdateTagDTO:CreateTagDTO
    {
        public int TagID { get; set; }
    }
}
