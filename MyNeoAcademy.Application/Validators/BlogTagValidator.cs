﻿using FluentValidation;
using MyNeoAcademy.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Validators
{
    public class CreateBlogTagValidator : AbstractValidator<CreateBlogTagDTO>
    {
        public CreateBlogTagValidator()
        {
            RuleFor(x => x.BlogID)
                .NotEmpty().WithMessage("Blog selection cannot be empty.")
                .GreaterThan(0).WithMessage("Please select a valid blog (title).");

            RuleFor(x => x.TagID)
                .NotEmpty().WithMessage("Tag selection cannot be empty.")
                .GreaterThan(0).WithMessage("Please select a valid tag (name).");

            RuleFor(x => x)
                .Must(BeDifferentBlogTag)
                .WithMessage("The selected blog and tag might already be associated.");


            RuleFor(x => x.BlogID.ToString())
                .MaximumLength(10)
                .WithMessage("The Blog ID value appears to be too long. This might indicate a system error.");

            RuleFor(x => x.TagID.ToString())
                .MaximumLength(10)
                .WithMessage("The Tag ID value appears to be too long. This might indicate a system error.");
        }


        private bool BeDifferentBlogTag(CreateBlogTagDTO dto)
        {

            return dto.BlogID != dto.TagID; 
        }
    }


    public class UpdateBlogTagValidator : AbstractValidator<UpdateBlogTagDTO>
    {
        public UpdateBlogTagValidator()
        {
            RuleFor(x => x.BlogTagID)
                .GreaterThan(0)
                .WithMessage("Please provide a valid BlogTag ID.");

            Include(new CreateBlogTagValidator());
        }
    }
}
