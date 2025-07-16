using FluentValidation;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.TestimonialValidator
{
    public class CreateTestimonialWithFileValidator : AbstractValidator<CreateTestimonialWithFileDTO>
    {
        public CreateTestimonialWithFileValidator()
        {
            Include(new CreateTestimonialValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }
}
