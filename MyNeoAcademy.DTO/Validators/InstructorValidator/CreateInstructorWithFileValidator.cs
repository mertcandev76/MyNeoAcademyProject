using FluentValidation;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using MyNeoAcademy.DTO.Validators.SliderValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.InstructorValidator
{
    public class CreateInstructorWithFileValidator : AbstractValidator<CreateInstructorWithFileDTO>
    {
        public CreateInstructorWithFileValidator()
        {
            Include(new CreateInstructorValidator());

            RuleFor(x => x.ImageFile)
          .NotNull().WithMessage("Bir görsel seçmelisiniz.")
          .Must(file => file != null && file.ContentType.StartsWith("image/"))
          .WithMessage("Yüklenen dosya bir görsel olmalıdır.");
        }
    }
}
