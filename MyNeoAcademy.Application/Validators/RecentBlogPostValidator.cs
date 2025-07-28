using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateRecentBlogPostValidator : AbstractValidator<CreateRecentBlogPostDTO>
    {
        public CreateRecentBlogPostValidator()
        {
            RuleFor(x => x.CompactTitle)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(100).WithMessage("Title must be at most 100 characters.");

        }
    }
    public class CreateRecentBlogPostWithFileValidator : AbstractValidator<CreateRecentBlogPostWithFileDTO>
    {
        public CreateRecentBlogPostWithFileValidator()
        {
            Include(new CreateRecentBlogPostValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }
    public class UpdateRecentBlogPostValidator : AbstractValidator<UpdateRecentBlogPostDTO>
    {
        public UpdateRecentBlogPostValidator()
        {
            RuleFor(x => x.RecentBlogPostID)
       .GreaterThan(0).WithMessage("Invalid RecenBlogPost ID.");

            Include(new CreateRecentBlogPostValidator());
        }
    }
    public class UpdateRecentBlogPostWithFileValidator : AbstractValidator<UpdateRecentBlogPostWithFileDTO>
    {
        public UpdateRecentBlogPostWithFileValidator()
        {
            RuleFor(x => x.RecentBlogPostID)
            .GreaterThan(0).WithMessage("Invalid RecenBlogPost ID.");

            Include(new CreateRecentBlogPostWithFileValidator());
        }
    }
}
