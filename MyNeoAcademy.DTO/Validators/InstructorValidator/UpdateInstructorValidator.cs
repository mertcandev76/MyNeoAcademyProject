using FluentValidation;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.InstructorValidator
{
    public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorDTO>
    {
        public UpdateInstructorValidator()
        {
            RuleFor(x => x.InstructorID)
            .GreaterThan(0).WithMessage("Invalid Instructor ID.");

            Include(new CreateInstructorValidator());
        }
    }
}
