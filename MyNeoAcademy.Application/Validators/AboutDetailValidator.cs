using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateAboutDetailValidator : AbstractValidator<CreateAboutDetailDTO>
    {
        public CreateAboutDetailValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("The title field is required.")
               .MaximumLength(150).WithMessage("The title must not exceed 150 characters.");

            RuleFor(x => x.Paragraph1)
                .NotEmpty().WithMessage("The first paragraph is required.")
                .MaximumLength(1000).WithMessage("The first paragraph must not exceed 1000 characters.");

            RuleFor(x => x.Paragraph2)
                .MaximumLength(1000).WithMessage("The second paragraph must not exceed 1000 characters.");
        }

    }

    public class UpdateAboutDetailValidator : AbstractValidator<UpdateAboutDetailDTO>
    {
        public UpdateAboutDetailValidator()
        {
            RuleFor(x => x.AboutDetailID)
        .GreaterThan(0).WithMessage("Invalid AboutDetail ID.");

            Include(new CreateAboutDetailValidator());
        }
    }
}
