using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.AboutValidator
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDTO>
    {
        public CreateAboutValidator()
        {
            // Genel metin alanları
            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Subtitle)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Alt başlık alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Alt başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MinimumLength(50).WithMessage("Açıklama en az 50 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

            RuleFor(x => x.ButtonText)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Buton metni boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Buton metni en fazla 50 karakter olabilir.");

            RuleFor(x => x.ButtonLink)
                .Must(BeAValidUrl)
                .WithMessage("Buton linki geçerli bir URL formatında olmalıdır (https://...).");

            // Görsel URL kontrolleri
            RuleFor(x => x.ImageFrontUrl)
                .Must(BeAValidUrlOrEmpty)
                .WithMessage("Ön görsel için geçerli bir URL giriniz (https://...).");

            RuleFor(x => x.ImageBackUrl)
                .Must(BeAValidUrlOrEmpty)
                .WithMessage("Arka görsel için geçerli bir URL giriniz (https://...).");
        }

        // Yardımcı validasyon metotları
        private bool BeAValidUrl(string? url)
        {
            return !string.IsNullOrWhiteSpace(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        private bool BeAValidUrlOrEmpty(string? url)
        {
            return string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
