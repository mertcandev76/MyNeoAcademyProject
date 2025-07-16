using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.AboutValidator
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDTO>
    {
        public CreateAboutValidator()
        {
            RuleFor(x => x.Subtitle)
            .NotEmpty().WithMessage("Subtitle cannot be empty.")
            .MaximumLength(100).WithMessage("Subtitle cannot exceed 100 characters.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");

            RuleFor(x => x.ButtonText)
                .MaximumLength(50).WithMessage("Button text cannot exceed 50 characters.");

            RuleFor(x => x.ButtonLink)
                .MaximumLength(200).WithMessage("Button link cannot exceed 200 characters.")
                .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrWhiteSpace(x.ButtonLink))
                .WithMessage("Button link must be a valid URL.");

            RuleFor(x => x.ImageFrontUrl)
                .MaximumLength(300).WithMessage("Front image URL cannot exceed 300 characters.");

            RuleFor(x => x.ImageBackUrl)
                .MaximumLength(300).WithMessage("Back image URL cannot exceed 300 characters.");
        }
    }
}
