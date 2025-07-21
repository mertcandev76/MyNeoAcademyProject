using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
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
    public class UpdateTestimonialValidator : AbstractValidator<UpdateTestimonialDTO>
    {
        public UpdateTestimonialValidator()
        {
            RuleFor(x => x.TestimonialID)
       .GreaterThan(0).WithMessage("Invalid Testimonial ID.");

            Include(new CreateTestimonialValidator());
        }
    }
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
