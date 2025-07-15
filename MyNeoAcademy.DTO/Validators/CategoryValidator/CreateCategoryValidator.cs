using FluentValidation;
using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.CategoryValidator
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
    .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
    .MaximumLength(100).WithMessage("Kategori adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                 .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.IconClass)
                .NotEmpty().WithMessage("İkon sınıfı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("İkon sınıfı en fazla 50 karakter olabilir.")
                .Matches(@"^[a-zA-Z0-9\-_\s]+$").WithMessage("İkon sınıfı sadece harf, rakam, boşluk, '-' veya '_' içerebilir.");
        }
    }
}
