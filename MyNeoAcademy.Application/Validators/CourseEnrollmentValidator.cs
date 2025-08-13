using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateCourseEnrollmentValidator : AbstractValidator<CreateCourseEnrollmentDTO>
    {
        public CreateCourseEnrollmentValidator()
        {
            RuleFor(x => x.CourseID)
                .GreaterThan(0)
                .WithMessage("Please enter a valid course ID.");

            RuleFor(x => x.AppUserID)
                .GreaterThan(0)
                .WithMessage("Please enter a valid user ID.");
        }
    }
}

