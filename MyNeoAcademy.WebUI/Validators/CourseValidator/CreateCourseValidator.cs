using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CourseDTOs;

namespace MyNeoAcademy.WebUI.Validators.CourseValidator
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Kurs adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kurs adı en az 3 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Kurs adı en fazla 100 karakter olabilir.")
                .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
                    .WithMessage("Kurs adı sadece boşluklardan oluşamaz.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250).WithMessage("Resim URL'si en fazla 250 karakter olabilir.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir URL giriniz.");

            RuleFor(x => x.CourseCategoryID)
                .GreaterThan(0).WithMessage("Lütfen geçerli bir kurs kategorisi seçiniz.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
        }
    }
}
