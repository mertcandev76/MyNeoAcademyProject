using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.CourseValidator
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDTO>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseID)
            .GreaterThan(0).WithMessage("Invalid  Course ID.");

            Include(new CreateCourseValidator());
        }
    }
}
