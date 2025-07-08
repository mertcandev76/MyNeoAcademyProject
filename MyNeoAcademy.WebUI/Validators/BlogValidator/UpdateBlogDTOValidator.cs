using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.WebUI.Validators.BannerValidator;

namespace MyNeoAcademy.WebUI.Validators.BlogValidator
{
    public class UpdateBlogDTOValidator : AbstractValidator<UpdateBlogDTO>
    {
        public UpdateBlogDTOValidator()
        {
            RuleFor(x => x.BlogID)
              .GreaterThan(0).WithMessage("Geçersiz blog ID.");


            Include(new CreateBlogDTOValidator()); // Tüm create kuralları burada da geçerli
        }
    }
}
