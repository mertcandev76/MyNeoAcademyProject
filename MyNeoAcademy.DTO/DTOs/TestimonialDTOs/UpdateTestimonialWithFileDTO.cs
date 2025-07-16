using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.TestimonialDTOs
{
    public class UpdateTestimonialWithFileDTO : CreateTestimonialWithFileDTO
    {
        public int TestimonialID { get; set; }
    }
}
