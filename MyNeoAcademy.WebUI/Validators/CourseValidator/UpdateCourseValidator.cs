using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;

namespace MyNeoAcademy.WebUI.Validators.CourseValidator
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDTO>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseID)
                .GreaterThan(0).WithMessage("Geçersiz kurs ID.");

            Include(new CreateCourseValidator()); // Tüm CreateCourseValidator kuralları burada da geçerli
        }
    }
}
