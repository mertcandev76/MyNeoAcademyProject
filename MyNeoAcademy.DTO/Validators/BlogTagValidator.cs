using FluentValidation;
using MyNeoAcademy.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators
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
            // This check should be handled in the API/database layer

            RuleFor(x => x.BlogID.ToString())
                .MaximumLength(10)
                .WithMessage("The Blog ID value appears to be too long. This might indicate a system error."); // Theoretical but protective

            RuleFor(x => x.TagID.ToString())
                .MaximumLength(10)
                .WithMessage("The Tag ID value appears to be too long. This might indicate a system error.");
        }

        // In a real system, this check should be performed against the database
        private bool BeDifferentBlogTag(CreateBlogTagDTO dto)
        {
            // This is just a placeholder logic. A proper check should be done against the data store.
            // Otherwise, the system may accept duplicate blog-tag relationships.
            return dto.BlogID != dto.TagID; // Dummy rule: prevent selecting the same ID for both Blog and Tag
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
