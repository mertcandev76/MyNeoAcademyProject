using FluentValidation;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;

namespace MyNeoAcademy.WebUI.Validators.AboutValidator
{
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutDTO>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.AboutID)
                .GreaterThan(0).WithMessage("Geçersiz About ID.");

            Include(new CreateAboutValidator()); // Create validator kuralları da dahil edilir
        }
    }
}
