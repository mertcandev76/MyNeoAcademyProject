using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Author name is required.")
               .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250).WithMessage("Image URL must not exceed 250 characters.")
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.ImageUrl))
                .WithMessage("Image URL must be a valid URL.");

            RuleFor(x => x.FacebookUrl)
                .MaximumLength(250).WithMessage("Facebook URL must not exceed 250 characters.")
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.FacebookUrl))
                .WithMessage("Facebook URL must be a valid URL.");

            RuleFor(x => x.TwitterUrl)
                .MaximumLength(250).WithMessage("Twitter URL must not exceed 250 characters.")
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.TwitterUrl))
                .WithMessage("Twitter URL must be a valid URL.");

            RuleFor(x => x.WebsiteUrl)
                .MaximumLength(250).WithMessage("Website URL must not exceed 250 characters.")
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.WebsiteUrl))
                .WithMessage("Website URL must be a valid URL.");
        }
        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }

    public class CreateAuthorWithFileValidator : AbstractValidator<CreateAuthorWithFileDTO>
    {
        public CreateAuthorWithFileValidator()
        {
            Include(new CreateAuthorValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }

    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDTO>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.AuthorID)
.GreaterThan(0).WithMessage("Invalid Author ID.");

            Include(new CreateAuthorValidator());
        }
    }

    public class UpdateAuthorWithFileValidator : AbstractValidator<UpdateAuthorWithFileDTO>
    {
        public UpdateAuthorWithFileValidator()
        {
            RuleFor(x => x.AuthorID)
       .GreaterThan(0).WithMessage("Invalid Instructor ID.");

            Include(new CreateAuthorWithFileValidator());
        }
    }
}
