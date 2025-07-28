using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Common;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
    public class UpdateTestimonialDTO : CreateTestimonialDTO,IHasId
    {
        public int TestimonialID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => TestimonialID;
            set => TestimonialID = value;
        }
    }
    public class UpdateTestimonialWithFileDTO : CreateTestimonialWithFileDTO,IHasId
    {
        public int TestimonialID { get; set; }
        public int Id
        {
            get => TestimonialID;
            set => TestimonialID = value;
        }
    }
}
