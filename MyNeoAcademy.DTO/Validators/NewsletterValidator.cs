using FluentValidation;
using MyNeoAcademy.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators
{
 
    public class CreateNewsletterValidator : AbstractValidator<CreateNewsletterDTO>
    {
        public CreateNewsletterValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(100).WithMessage("E-posta adresi en fazla 100 karakter olabilir.");
        }
       
    }

    public class UpdateNewsletterValidator : AbstractValidator<UpdateNewsletterDTO>
    {
        public UpdateNewsletterValidator()
        {
            RuleFor(x => x.NewsletterID)
        .GreaterThan(0).WithMessage("Invalid Newsletter ID.");

            Include(new CreateNewsletterValidator()); 
        }
    }
}
