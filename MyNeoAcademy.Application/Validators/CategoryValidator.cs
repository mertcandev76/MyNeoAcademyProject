using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Category name can be at most 100 characters long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .MaximumLength(500).WithMessage("Description can be at most 500 characters long.");

            RuleFor(x => x.IconClass)
                .NotEmpty().WithMessage("Icon class cannot be empty.")
                .MaximumLength(50).WithMessage("Icon class can be at most 50 characters long.")
                .Matches(@"^[a-zA-Z0-9\-_\s]+$").WithMessage("Icon class can only contain letters, numbers, spaces, '-' or '_'.");
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.CategoryID)
        .GreaterThan(0).WithMessage("Invalid category ID.");

            Include(new CreateCategoryValidator()); // Create kuralları da geçerli
        }
    }
}
