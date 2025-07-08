using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;

namespace MyNeoAcademy.WebUI.Validators.SocialMediaValidator
{
    public class CreateSocialMediaValidator : AbstractValidator<CreateSocialMediaDTO>
    {
        public CreateSocialMediaValidator()
        {
            // Başlık (Title) kontrolü
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter olabilir.");

            // İkon (Icon) kontrolü
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("İkon alanı boş bırakılamaz.")
                .Matches(@"^fa[b,s,r,l]? fa-[a-z0-9\-]+$").WithMessage("Lütfen geçerli bir Font Awesome ikon sınıfı girin. Örn: fab fa-twitter");

            // Bağlantı (IconUrl) kontrolü
            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("Bağlantı URL'si boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Bağlantı URL'si en fazla 250 karakter olabilir.")
                .Must(BeAValidUrl).WithMessage("Geçerli bir URL giriniz. Örneğin: https://twitter.com/kullanici");
        }

        // URL geçerlilik kontrolü için özel metot
        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var validatedUri)
                && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }
    }
    }

