using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutDTOs
{
    public class CreateAboutWithFileDTO
    {
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
        public IFormFile? ImageFrontFile { get; set; }
        public IFormFile? ImageBackFile { get; set; }
    }
}
