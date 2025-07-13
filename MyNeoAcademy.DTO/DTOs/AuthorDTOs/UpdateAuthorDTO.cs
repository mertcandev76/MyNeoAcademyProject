using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AuthorDTOs
{
    public class UpdateAuthorDTO: CreateAuthorDTO
    {
        public int AuthorID { get; set; }
    }
}
