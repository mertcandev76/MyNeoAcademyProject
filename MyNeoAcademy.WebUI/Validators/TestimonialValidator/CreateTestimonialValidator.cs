using FluentValidation;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;

namespace MyNeoAcademy.WebUI.Validators.TestimonialValidator
{
    public class CreateTestimonialValidator : AbstractValidator<CreateTestimonialDTO>
    {
        public CreateTestimonialValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad soyad boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ad soyad en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ad soyad en fazla 100 karakter olabilir.")
                .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
                    .WithMessage("Ad soyad sadece boşluklardan oluşamaz.");

            RuleFor(x => x.Title)
                .MaximumLength(100).WithMessage("Unvan en fazla 100 karakter olabilir.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250).WithMessage("Resim URL'si en fazla 250 karakter olabilir.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir URL giriniz.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Yorum alanı boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Yorum en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Yorum en fazla 1000 karakter olabilir.");

            RuleFor(x => x.Star)
                .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır.");
        }
    }
}
