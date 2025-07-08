using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;

namespace MyNeoAcademy.WebUI.Validators.BannerValidator
{
    public class CreateBannerValidator : AbstractValidator<CreateBannerDTO>
    {
        public CreateBannerValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Banner başlığı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Banner başlığı en az 3 karakter olmalıdır.")
                .MaximumLength(150).WithMessage("Banner başlığı en fazla 150 karakter olabilir.")
                .Must(title => !string.IsNullOrWhiteSpace(title?.Trim()))
                    .WithMessage("Banner başlığı sadece boşluklardan oluşamaz.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Banner görseli boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Görsel URL'si en fazla 250 karakter olabilir.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir URL giriniz.");
        }
    }
}
