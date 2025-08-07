using FluentValidation;
using MyNeoAcademy.Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators.Role
{
    public class RoleBaseValidator<T> : AbstractValidator<T> where T : RoleBaseDTO
    {
        public RoleBaseValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Role name cannot be empty")
           .Length(3, 50).WithMessage("Role name must be between 3 and 50 characters long");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description cannot be longer than 200 characters");

        }
    }

    public class CreateRoleValidator : RoleBaseValidator<CreateRoleDTO>
    {
        public CreateRoleValidator()
        {

            RuleFor(x => x.Name)
                .Matches(@"^[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$")
                .WithMessage("Role name can only contain letters and spaces");


            RuleFor(x => x.Description)
                .Must(desc => string.IsNullOrWhiteSpace(desc) || !string.IsNullOrWhiteSpace(desc.Trim()))
                .WithMessage("Description cannot consist of only whitespace characters");
        }
    }


    public class UpdateRoleValidator : RoleBaseValidator<UpdateRoleDTO>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid role ID");

        }
    }


}
