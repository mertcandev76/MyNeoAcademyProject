using FluentValidation;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.TestimonialValidator
{
    public class UpdateTestimonialWithFileValidator : AbstractValidator<UpdateTestimonialWithFileDTO>
    {
        public UpdateTestimonialWithFileValidator()
        {
            RuleFor(x => x.TestimonialID)
            .GreaterThan(0).WithMessage("Invalid Testimonial ID.");

            Include(new CreateTestimonialWithFileValidator());
        }
    }
}
