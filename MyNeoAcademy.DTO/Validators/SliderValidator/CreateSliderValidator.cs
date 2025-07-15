using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.SliderValidator
{
    public class CreateSliderValidator : AbstractValidator<CreateSliderDTO>
    {
        public CreateSliderValidator()
        {
            RuleFor(x => x.Title)
                  .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
                  .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.SubTitle)
                 .NotEmpty().WithMessage("Alt başlık alanı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Alt başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.ButtonText)
                       .NotEmpty().WithMessage("Buton Metni alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Buton metni en fazla 50 karakter olabilir.");

            RuleFor(x => x.ButtonUrl)
                       .NotEmpty().WithMessage("Buton URL alanı boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Buton URL'si en fazla 250 karakter olabilir.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(x => !string.IsNullOrWhiteSpace(x.ButtonUrl))
                .WithMessage("Geçersiz URL formatı.");
        }
    }
}
