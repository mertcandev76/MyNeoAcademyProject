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
        public string FullName { get; set; } = null!;   // Hem AuthorName hem FullName yerine tek bir isim
        public string? Title { get; set; }       // Hem AuthorTitle hem Title
        public string? ImageUrl { get; set; }    // Hem AuthorImageUrl hem ImageUrl
        public string? Content { get; set; }     // Hem Message hem Content
        public int Rating { get; set; }         // Hem StarRating hem Rating
    }
}

