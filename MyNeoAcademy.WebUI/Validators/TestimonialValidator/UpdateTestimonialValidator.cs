using FluentValidation;
using MyNeoAcademy.DTO.DTOs.TestimonialDTOs;

namespace MyNeoAcademy.WebUI.Validators.TestimonialValidator
{
    public class UpdateTestimonialValidator : AbstractValidator<UpdateTestimonialDTO>
    {
        public UpdateTestimonialValidator()
        {
            RuleFor(x => x.TestimonialID)
                .GreaterThan(0).WithMessage("Geçersiz referans ID.");

            Include(new CreateTestimonialValidator()); // Tüm Create kuralları dahil edilir
        }
    }
}
