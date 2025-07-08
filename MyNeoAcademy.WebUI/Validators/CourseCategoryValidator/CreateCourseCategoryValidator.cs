using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs;
using System.Text.RegularExpressions;

namespace MyNeoAcademy.WebUI.Validators.CourseCategoryValidator
{
    public class CreateCourseCategoryValidator : AbstractValidator<CreateCourseCategoryDTO>
    {
        public CreateCourseCategoryValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
              .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
              .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.")
              .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
                  .WithMessage("Kategori adı sadece boşluklardan oluşamaz.")
              .Matches(@"^[a-zA-Z0-9ğüşöçıİĞÜŞÖÇ\s\-]+$")
                  .WithMessage("Kategori adı geçersiz karakterler içeriyor.")
              .Must(name => !Regex.IsMatch(name, @"^\d+$"))
                  .WithMessage("Kategori adı yalnızca sayılardan oluşamaz.");

            RuleFor(x => x.Icon)
                .MaximumLength(100).WithMessage("İkon adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
