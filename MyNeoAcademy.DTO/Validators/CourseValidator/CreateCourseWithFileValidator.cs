using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;
using MyNeoAcademy.DTO.Validators.CourseValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.CourseValidator
{
    public class CreateCourseWithFileValidator : AbstractValidator<CreateCourseWithFileDTO>
    {
        public CreateCourseWithFileValidator()
        {
            Include(new CreateCourseValidator());

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("You must select an image.")
                .Must(file => file != null && file.ContentType.StartsWith("image/"))
                .WithMessage("The uploaded file must be an image.");
        }
    }
}
