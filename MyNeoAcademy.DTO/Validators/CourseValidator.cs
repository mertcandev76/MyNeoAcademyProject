using FluentValidation;
using MyNeoAcademy.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("ImageUrl must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.ImageUrl));

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");

            RuleFor(x => x.ReviewCount)
                .GreaterThanOrEqualTo(0).WithMessage("ReviewCount cannot be negative.");

            RuleFor(x => x.StudentCount)
                .GreaterThanOrEqualTo(0).WithMessage("StudentCount cannot be negative.");

            RuleFor(x => x.LikeCount)
                .GreaterThanOrEqualTo(0).WithMessage("LikeCount cannot be negative.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.CategoryID)
                .GreaterThan(0).WithMessage("CategoryID must be greater than zero.");

            RuleFor(x => x.InstructorID)
                .GreaterThan(0).WithMessage("InstructorID must be greater than zero.");

        }

    }

    public class CreateCourseWithFileValidator : AbstractValidator<CreateCourseWithFileDTO>
    {
        public CreateCourseWithFileValidator()
        {
            Include(new CreateCourseValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }

    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDTO>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseID)
            .GreaterThan(0).WithMessage("Invalid  Course ID.");

            Include(new CreateCourseValidator());
        }
    }
    public class UpdateCourseWithFileValidator : AbstractValidator<UpdateCourseWithFileDTO>
    {
        public UpdateCourseWithFileValidator()
        {
            RuleFor(x => x.CourseID)
            .GreaterThan(0).WithMessage("Invalid Course ID.");

            Include(new CreateCourseWithFileValidator());
        }
    }
}
