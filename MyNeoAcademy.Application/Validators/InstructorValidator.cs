using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateInstructorValidator : AbstractValidator<CreateInstructorDTO>
    {
        public CreateInstructorValidator()
        {

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");


            RuleFor(x => x.Title)
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));


            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Bio));


            RuleFor(x => x.ImageUrl)
                .Must(BeAValidUrl).WithMessage("Image URL must be a valid URL.")
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));


            RuleFor(x => x.FacebookUrl)
                .Must(BeAValidUrl).WithMessage("Facebook URL must be a valid URL.")
                .Must(url => url!.Contains("facebook.com")).WithMessage("Facebook URL must contain 'facebook.com'.")
                .When(x => !string.IsNullOrWhiteSpace(x.FacebookUrl));


            RuleFor(x => x.TwitterUrl)
                .Must(BeAValidUrl).WithMessage("Twitter URL must be a valid URL.")
                .Must(url => url!.Contains("twitter.com")).WithMessage("Twitter URL must contain 'twitter.com'.")
                .When(x => !string.IsNullOrWhiteSpace(x.TwitterUrl));


            RuleFor(x => x.WebsiteUrl)
                .Must(BeAValidUrl).WithMessage("Website URL must be a valid URL.")
                .When(x => !string.IsNullOrWhiteSpace(x.WebsiteUrl));
        }



        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    public class CreateInstructorWithFileValidator : AbstractValidator<CreateInstructorWithFileDTO>
    {
        public CreateInstructorWithFileValidator()
        {
            Include(new CreateInstructorValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorDTO>
    {
        public UpdateInstructorValidator()
        {
            RuleFor(x => x.InstructorID)
            .GreaterThan(0).WithMessage("Invalid Instructor ID.");

            Include(new CreateInstructorValidator());
        }
    }
    public class UpdateInstructorWithFileValidator : AbstractValidator<UpdateInstructorWithFileDTO>
    {
        public UpdateInstructorWithFileValidator()
        {
            RuleFor(x => x.InstructorID)
                   .GreaterThan(0).WithMessage("Invalid Instructor ID.");

            Include(new CreateInstructorWithFileValidator());
        }
    }
}
