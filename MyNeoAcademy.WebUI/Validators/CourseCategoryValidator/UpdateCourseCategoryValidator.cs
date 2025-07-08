using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;

namespace MyNeoAcademy.WebUI.Validators.CourseCategoryValidator
{
    public class UpdateCourseCategoryValidator : AbstractValidator<UpdateCourseCategoryDTO>
    {
        public UpdateCourseCategoryValidator()
        {
            RuleFor(x => x.CourseCategoryID)
                .GreaterThan(0).WithMessage("Geçersiz kategori ID.");

            Include(new CreateCourseCategoryValidator()); // Create kurallarını dahil et
        }
    }
}
