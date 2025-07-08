using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SubscriberDTOs;

namespace MyNeoAcademy.WebUI.Validators.SubscriberValidator
{
    public class UpdateSubscriberValidator : AbstractValidator<UpdateSubscriberDTO>
    {
        public UpdateSubscriberValidator()
        {
            RuleFor(x => x.SubscriberID)
                .GreaterThan(0).WithMessage("Geçersiz abone ID.");

            Include(new CreateSubscriberValidator()); // Tüm Create kuralları burada da geçerli
        }
    }
}
