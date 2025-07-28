using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Testimonial
    {

        public int TestimonialID { get; set; }
        public string FullName { get; set; } = null!;  
        public string? Title { get; set; }      
        public string? ImageUrl { get; set; }    
        public string? Content { get; set; }   
        public int Rating { get; set; }        
    }
}

