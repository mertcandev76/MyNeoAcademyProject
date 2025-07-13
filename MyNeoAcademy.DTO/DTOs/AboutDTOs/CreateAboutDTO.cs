using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutDTOs
{
    public class CreateAboutDTO
    {
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
        public string? ImageFrontUrl { get; set; }
        public string? ImageBackUrl { get; set; }
    }
}
