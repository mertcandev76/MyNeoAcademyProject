using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTagDTO>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name cannot be empty.")
                .MinimumLength(3).WithMessage("Tag name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Tag name can be at most 100 characters long.");
        }
    }

    public class UpdateTagValidator : AbstractValidator<UpdateTagDTO>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.TagID)
        .GreaterThan(0).WithMessage("Invalid Tag ID.");

            Include(new CreateTagValidator()); // Create kuralları da geçerli
        }
    }
}
