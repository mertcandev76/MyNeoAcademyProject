using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.CategoryValidator
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.CategoryID)
         .GreaterThan(0).WithMessage("Geçersiz kategori ID.");

            Include(new CreateCategoryValidator()); // Create kuralları da geçerli
        }
    }
}
