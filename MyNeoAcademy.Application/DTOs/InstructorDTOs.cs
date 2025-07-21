using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class InstructorReferenceDTO
    {
        public int InstructorID { get; set; }
        public string FullName { get; set; } = null!;
    }
    public class CreateInstructorDTO
    {


        public string FullName { get; set; } = null!;

        public string? Title { get; set; }

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public string? FacebookUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? WebsiteUrl { get; set; }


    }
    public class CreateInstructorWithFileDTO : CreateInstructorDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class ResultInstructorDTO : CreateInstructorDTO
    {
        public int InstructorID { get; set; }
        public List<CourseReferenceDTO> Courses { get; set; } = new List<CourseReferenceDTO>();
    }
    public class UpdateInstructorDTO : CreateInstructorDTO
    {
        public int InstructorID { get; set; }
    }
    public class UpdateInstructorWithFileDTO : CreateInstructorWithFileDTO
    {
        public int InstructorID { get; set; }
    }
}
