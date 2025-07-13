using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.AboutValidator
{
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutDTO>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.AboutID)
                .GreaterThan(0).WithMessage("Geçersiz About ID.");

            Include(new CreateAboutValidator()); // Create validator kuralları da dahil edilir
        }
    }
}
