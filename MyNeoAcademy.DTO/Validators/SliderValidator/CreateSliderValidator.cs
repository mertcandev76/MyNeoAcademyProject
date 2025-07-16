using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.SliderValidator
{
    public class CreateSliderValidator : AbstractValidator<CreateSliderDTO>
    {
        public CreateSliderValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title field cannot be empty.")
                .MaximumLength(100).WithMessage("Title can be at most 100 characters long.");

            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("Subtitle field cannot be empty.")
                .MaximumLength(150).WithMessage("Subtitle can be at most 150 characters long.");

            RuleFor(x => x.ButtonText)
                .NotEmpty().WithMessage("Button text field cannot be empty.")
                .MaximumLength(50).WithMessage("Button text can be at most 50 characters long.");

            RuleFor(x => x.ButtonUrl)
                .NotEmpty().WithMessage("Button URL field cannot be empty.")
                .MaximumLength(250).WithMessage("Button URL can be at most 250 characters long.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(x => !string.IsNullOrWhiteSpace(x.ButtonUrl))
                .WithMessage("Invalid URL format.");
        }
    }
}
