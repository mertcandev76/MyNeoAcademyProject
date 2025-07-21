using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CreateTestimonialDTO
    {

        public string FullName { get; set; } = null!;
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
    }
    public class CreateTestimonialWithFileDTO : CreateTestimonialDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class ResultTestimonialDTO : CreateTestimonialDTO
    {
        public int TestimonialID { get; set; }
    }
    public class UpdateTestimonialDTO : CreateTestimonialDTO
    {
        public int TestimonialID { get; set; }
    }
    public class UpdateTestimonialWithFileDTO : CreateTestimonialWithFileDTO
    {
        public int TestimonialID { get; set; }
    }
}
