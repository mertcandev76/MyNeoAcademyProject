using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.WebUI.Validators.BannerValidator;
using System.Text.RegularExpressions;

namespace MyNeoAcademy.WebUI.Validators.BlogCategoryValidator
{
    public class UpdateBlogCategoryValidator : AbstractValidator<UpdateBlogCategoryDTO>
    {
        public UpdateBlogCategoryValidator()
        {
            RuleFor(x => x.BlogCategoryID)
      .GreaterThan(0).WithMessage("Geçersiz Blog Kategori ID.");


            Include(new CreateBlogCategoryValidator()); // Tüm create kuralları burada da geçerli
        }
    }
}
