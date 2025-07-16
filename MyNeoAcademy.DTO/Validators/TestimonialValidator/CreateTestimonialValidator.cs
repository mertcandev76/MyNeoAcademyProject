using FluentValidation;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.TestimonialValidator
{
    public class CreateTestimonialValidator : AbstractValidator<CreateTestimonialDTO>
    {
        public CreateTestimonialValidator()
        {
            RuleFor(x => x.FullName)
    .NotEmpty().WithMessage("Full name is required.")
    .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.")
    .Matches(@"^[a-zA-ZÇçĞğİıÖöŞşÜü\s]+$").WithMessage("Full name can only contain letters and spaces.");

            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage(x => $"'{x.Rating}' is not a valid rating. Rating must be between 1 and 5.");

        }
    }
}
