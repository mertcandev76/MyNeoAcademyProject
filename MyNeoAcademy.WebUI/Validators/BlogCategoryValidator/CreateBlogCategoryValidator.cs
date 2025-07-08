using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using System.Text.RegularExpressions;

namespace MyNeoAcademy.WebUI.Validators.BlogCategoryValidator
{
    public class CreateBlogCategoryValidator : AbstractValidator<CreateBlogCategoryDTO>
    {
        public CreateBlogCategoryValidator()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
                  .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                  .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.")
                  .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
                      .WithMessage("Kategori adı sadece boşluklardan oluşamaz.")
                  .Matches(@"^[a-zA-Z0-9ğüşöçıİĞÜŞÖÇ\s\-]+$")
                      .WithMessage("Kategori adı geçersiz karakterler içeriyor.")
                  .Must(name => !Regex.IsMatch(name, @"^\d+$"))
                      .WithMessage("Kategori adı yalnızca sayılardan oluşamaz.");
        }
    }
}
