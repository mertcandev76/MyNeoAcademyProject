using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;

namespace MyNeoAcademy.WebUI.Validators.SubscriberValidator
{
    public class CreateSubscriberValidator : AbstractValidator<CreateSubscriberDTO>
    {
        public CreateSubscriberValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(150).WithMessage("E-posta adresi en fazla 150 karakter olabilir.");
        }
    }
}
