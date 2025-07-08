using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;

namespace MyNeoAcademy.WebUI.Validators.BlogValidator
{
    public class CreateBlogDTOValidator : AbstractValidator<CreateBlogDTO>
    {
        public CreateBlogDTOValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
              .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır.")
              .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.")
              .Must(title => !string.IsNullOrWhiteSpace(title?.Trim()))
                  .WithMessage("Başlık sadece boşluklardan oluşamaz.")
              .Matches(@"^[a-zA-Z0-9ğüşöçıİĞÜŞÖÇ\s\-]+$")
                  .WithMessage("Başlık geçersiz karakterler içeriyor.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş bırakılamaz.")
                .MinimumLength(20).WithMessage("İçerik en az 20 karakter olmalıdır.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL boş bırakılamaz.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir URL giriniz.");

            RuleFor(x => x.BlogDate)
                .NotEmpty().WithMessage("Blog tarihi boş bırakılamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Blog tarihi gelecekte olamaz.");

            RuleFor(x => x.BlogCategoryID)
                .GreaterThan(0).WithMessage("Lütfen bir kategori seçiniz.");
        }
    }
}
