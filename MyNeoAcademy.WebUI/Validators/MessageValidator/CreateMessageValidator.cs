using FluentValidation;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;

namespace MyNeoAcademy.WebUI.Validators.MessageValidator
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageDTO>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad soyad boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ad soyad en az 2 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Ad soyad en fazla 100 karakter olabilir.")
                .Must(name => !string.IsNullOrWhiteSpace(name?.Trim()))
                    .WithMessage("Ad soyad sadece boşluklardan oluşamaz.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Konu boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Konu en az 3 karakter olmalıdır.")
                .MaximumLength(150).WithMessage("Konu en fazla 150 karakter olabilir.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Mesaj içeriği en az 10 karakter olmalıdır.")
                .MaximumLength(1000).WithMessage("Mesaj içeriği en fazla 1000 karakter olabilir.");
        }
    }
}
