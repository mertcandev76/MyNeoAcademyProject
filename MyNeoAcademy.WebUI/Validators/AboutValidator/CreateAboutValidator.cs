using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;

namespace MyNeoAcademy.WebUI.Validators.AboutValidator
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDTO>
    {
        public CreateAboutValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MinimumLength(20).WithMessage("Açıklama en az 20 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

            RuleFor(x => x.ImageUrl1)
                .MaximumLength(250).WithMessage("1. görsel URL'si en fazla 250 karakter olabilir.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir 1. görsel URL'si giriniz.");

            RuleFor(x => x.ImageUrl2)
                .MaximumLength(250).WithMessage("2. görsel URL'si en fazla 250 karakter olabilir.")
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir 2. görsel URL'si giriniz.");

            RuleFor(x => x.Item1)
                .MaximumLength(200).WithMessage("Item1 en fazla 200 karakter olabilir.");

            RuleFor(x => x.Item2)
                .MaximumLength(200).WithMessage("Item2 en fazla 200 karakter olabilir.");

            RuleFor(x => x.Item3)
                .MaximumLength(200).WithMessage("Item3 en fazla 200 karakter olabilir.");

            RuleFor(x => x.Item4)
                .MaximumLength(200).WithMessage("Item4 en fazla 200 karakter olabilir.");
        }
    }
}
