using FluentValidation;
using MyNeoAcademy.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(50).WithMessage("Username can be at most 50 characters long.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment content cannot be empty.")
                .MaximumLength(500).WithMessage("Comment can be at most 500 characters long.");

            RuleFor(x => x.BlogID)
                .GreaterThan(0).WithMessage("A valid Blog ID must be provided.");

        }
    }

    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.CommentID)
           .GreaterThan(0).WithMessage("Invalid comment ID.");

            Include(new CreateCommentValidator()); 
        }
    }

    public class CreateCommentWithFileValidator : AbstractValidator<CreateCommentWithFileDTO>
    {
        public CreateCommentWithFileValidator()
        {
            Include(new CreateCommentValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }


    public class UpdateCommentWithFileValidator : AbstractValidator<UpdateCommentWithFileDTO>
    {
        public UpdateCommentWithFileValidator()
        {
            RuleFor(x => x.CommentID)
       .GreaterThan(0).WithMessage("Invalid Comment ID.");

            Include(new CreateCommentWithFileValidator());
        }
    }


}
