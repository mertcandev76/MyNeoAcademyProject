using FluentValidation;
using MyNeoAcademy.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators.User
{
    public class UpdateAppUserDTOValidator : AbstractValidator<UpdateAppUserDTO>
    {
        public UpdateAppUserDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");


        }
    }
    public class AssignRolesDTOValidator : AbstractValidator<AssignRolesDTO>
    {
        public AssignRolesDTOValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID girilmelidir.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Rol listesi boş olamaz.")
                .Must(r => r.Count > 0).WithMessage("En az bir rol seçilmelidir.");
        }
    }
}
