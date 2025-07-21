using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CreateAboutDetailDTO
    {
        public string? Title { get; set; }
        public string? Paragraph1 { get; set; }
        public string? Paragraph2 { get; set; }
    }   
    public class ResultAboutDetailDTO : CreateAboutDetailDTO
    {
        public int AboutDetailID { get; set; }

    }
    public class UpdateAboutDetailDTO : CreateAboutDetailDTO
    {
        public int AboutDetailID { get; set; }
    }
}
