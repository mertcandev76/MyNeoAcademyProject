using FluentValidation;
using MyNeoAcademy.DTO.DTOs.MessageDTOs;

namespace MyNeoAcademy.WebUI.Validators.MessageValidator
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageDTO>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.MessageID)
                .GreaterThan(0).WithMessage("Geçersiz mesaj ID.");

            Include(new CreateMessageValidator()); // CreateMessageValidator kurallarını da dahil eder
        }
    }
}
