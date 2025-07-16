using FluentValidation;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.SliderValidator
{
    public class UpdateSliderValidator : AbstractValidator<UpdateSliderDTO>
    {
        public UpdateSliderValidator()
        {
            RuleFor(x => x.SliderID)
                 .GreaterThan(0).WithMessage("Invalid  Slider ID.");

            Include(new CreateSliderValidator());
        }
    }
}
