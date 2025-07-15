using FluentValidation;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using MyNeoAcademy.DTO.Validators.SliderValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.InstructorValidator
{
    public class UpdateInstructorWithFileValidator : AbstractValidator<UpdateInstructorWithFileDTO>
    {
        public UpdateInstructorWithFileValidator()
        {
            RuleFor(x => x.InstructorID)
                   .GreaterThan(0).WithMessage("Geçersiz Instructor ID.");

            Include(new CreateInstructorWithFileValidator());
        }
    }
}
