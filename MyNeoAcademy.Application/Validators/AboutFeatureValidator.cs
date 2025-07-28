using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateAboutFeatureValidator : AbstractValidator<CreateAboutFeatureDTO>
    {
        public CreateAboutFeatureValidator()
        {
            RuleFor(x => x.IconClass)
                .NotEmpty().WithMessage("Icon class cannot be empty.")
                .MaximumLength(100).WithMessage("Icon class must not exceed 100 characters.");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Description text cannot be empty.")
                .MaximumLength(250).WithMessage("Description text must not exceed 250 characters.");

            RuleFor(x => x.AboutID)
                .GreaterThan(0).WithMessage("'About' alanı seçilmelidir.");
        }
    }

    public class UpdateAboutFeatureValidator : AbstractValidator<UpdateAboutFeatureDTO>
    {
        public UpdateAboutFeatureValidator()
        {
            RuleFor(x => x.AboutFeatureID)
                .GreaterThan(0).WithMessage("Invalid AboutFeature ID.");

            Include(new CreateAboutFeatureValidator());
        }
    }

}
