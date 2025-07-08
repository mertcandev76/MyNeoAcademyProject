using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SocialMediaDTOs;

namespace MyNeoAcademy.WebUI.Validators.SocialMediaValidator
{
    public class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaDTO>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(x => x.SocialMediaID)
                .GreaterThan(0).WithMessage("Geçersiz sosyal medya ID.");

            Include(new CreateSocialMediaValidator()); // Tüm Create kurallarını da dahil et
        }
    }
}
