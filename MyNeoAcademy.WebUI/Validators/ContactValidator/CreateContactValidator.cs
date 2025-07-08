using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;

namespace MyNeoAcademy.WebUI.Validators.ContactValidator
{
    public class CreateContactValidator : AbstractValidator<CreateContactDTO>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.MapUrl)
                .NotEmpty().WithMessage("Harita URL'si boş bırakılamaz.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    .WithMessage("Geçerli bir URL giriniz.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres bilgisi boş bırakılamaz.")
                .MinimumLength(5).WithMessage("Adres en az 5 karakter olmalıdır.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$")
                    .WithMessage("Telefon numarası geçersiz formatta. Örnek: +90 555 123 4567");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
