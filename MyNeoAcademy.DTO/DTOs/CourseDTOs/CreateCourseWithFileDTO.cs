using Microsoft.AspNetCore.Http;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class CreateCourseWithFileDTO : CreateCourseDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
}
