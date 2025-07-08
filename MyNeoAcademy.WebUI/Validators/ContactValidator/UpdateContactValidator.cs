using FluentValidation;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;

namespace MyNeoAcademy.WebUI.Validators.ContactValidator
{
    public class UpdateContactValidator : AbstractValidator<UpdateContactDTO>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.ContactID)
               .GreaterThan(0).WithMessage("Geçersiz Contact ID.");

            Include(new CreateContactValidator()); // Tüm CreateContact kuralları da geçerli
        }
    }
}
