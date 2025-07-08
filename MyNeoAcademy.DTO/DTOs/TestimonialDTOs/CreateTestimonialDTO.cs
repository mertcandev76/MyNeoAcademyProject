using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.TestimonialDTOs
{
    public class CreateTestimonialDTO
    {

        public string Name { get; set; } = null!;
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Comment { get; set; }
        public int Star { get; set; }
    }
}
