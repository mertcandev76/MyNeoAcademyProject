using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.Validators.InstructorValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.AboutValidator
{
    public class UpdateAboutWithFileValidator : AbstractValidator<UpdateAboutWithFileDTO>
    {
        public UpdateAboutWithFileValidator()
        {
            RuleFor(x => x.AboutID)
            .GreaterThan(0).WithMessage("Invalid About ID.");

            Include(new CreateAboutWithFileValidator());
        }
    }
}
