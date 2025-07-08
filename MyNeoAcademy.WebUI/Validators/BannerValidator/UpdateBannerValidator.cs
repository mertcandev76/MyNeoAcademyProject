using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BannerDTOs;

namespace MyNeoAcademy.WebUI.Validators.BannerValidator
{
    public class UpdateBannerValidator : AbstractValidator<UpdateBannerDTO>
    {
        public UpdateBannerValidator()
        {
            RuleFor(x => x.BannerID)
                .GreaterThan(0).WithMessage("Geçersiz banner ID.");

            Include(new CreateBannerValidator()); // Tüm create kuralları burada da geçerli
        }
    }
}
