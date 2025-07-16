using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutDTOs
{
    public class CreateAboutWithFileDTO : CreateAboutDTO
    {

        public IFormFile? ImageFrontFile { get; set; }
        public IFormFile? ImageBackFile { get; set; }
    }
}
