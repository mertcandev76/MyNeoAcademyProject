using Microsoft.AspNetCore.Http;
using MyNeoAcademy.DTO.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.TestimonialDTOs
{
    public class CreateTestimonialWithFileDTO : CreateTestimonialDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
}
