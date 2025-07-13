using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutDTOs
{
    public class UpdateAboutWithFileDTO : CreateAboutWithFileDTO
    {
        public int AboutID { get; set; }
    }
}
