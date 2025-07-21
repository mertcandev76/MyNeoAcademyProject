using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateBlogValidator : AbstractValidator<CreateBlogDTO>
    {
        public CreateBlogValidator()
        {
            RuleFor(x => x.Title)
      .NotEmpty().WithMessage("Title field cannot be empty.")
      .MaximumLength(100).WithMessage("Title can be at most 100 characters long.");

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage("Short description cannot be empty.")
                .MaximumLength(250).WithMessage("Short description can be at most 250 characters long.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content field cannot be empty.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250).WithMessage("Image URL can be at most 250 characters long.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Please enter a valid URL.");

            RuleFor(x => x.PublishDate)
                .NotEmpty().WithMessage("Publish date cannot be empty.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Publish date cannot be in the future.");

            RuleFor(x => x.AuthorID)
                .NotNull().WithMessage("Author selection is required.")
                .GreaterThan(0).WithMessage("A valid author ID must be provided.");

            RuleFor(x => x.CategoryID)
                .NotNull().WithMessage("Category selection is required.")
                .GreaterThan(0).WithMessage("A valid category ID must be provided.");

        }



    }

    public class CreateBlogWithFileValidator : AbstractValidator<CreateBlogWithFileDTO>
    {
        public CreateBlogWithFileValidator()
        {
            Include(new CreateBlogValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogDTO>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.BlogID)
            .GreaterThan(0).WithMessage("Invalid Blog ID.");

            Include(new CreateBlogValidator());
        }
    }
    public class UpdateBlogWithFileValidator : AbstractValidator<UpdateBlogWithFileDTO>
    {
        public UpdateBlogWithFileValidator()
        {
            RuleFor(x => x.BlogID)
                   .GreaterThan(0).WithMessage("Invalid Blog ID.");

            Include(new CreateBlogWithFileValidator());
        }
    }
}
