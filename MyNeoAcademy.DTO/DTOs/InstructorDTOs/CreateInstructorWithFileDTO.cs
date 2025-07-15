using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.InstructorDTOs
{
    public class CreateInstructorWithFileDTO:CreateInstructorDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
}
