using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators
{
    public class CreateAboutValidator : AbstractValidator<CreateAboutDTO>
    {
        public CreateAboutValidator()
        {
            RuleFor(x => x.Subtitle)
            .NotEmpty().WithMessage("Subtitle cannot be empty.")
            .MaximumLength(100).WithMessage("Subtitle cannot exceed 100 characters.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.");

            RuleFor(x => x.ButtonText)
                .MaximumLength(50).WithMessage("Button text cannot exceed 50 characters.");

            RuleFor(x => x.ButtonLink)
                .MaximumLength(200).WithMessage("Button link cannot exceed 200 characters.")
                .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrWhiteSpace(x.ButtonLink))
                .WithMessage("Button link must be a valid URL.");

            RuleFor(x => x.ImageFrontUrl)
                .MaximumLength(300).WithMessage("Front image URL cannot exceed 300 characters.");

            RuleFor(x => x.ImageBackUrl)
                .MaximumLength(300).WithMessage("Back image URL cannot exceed 300 characters.");
        }
    }
    public class CreateAboutWithFileValidator : AbstractValidator<CreateAboutWithFileDTO>
    {
        // 👉 Dosya boyutu limiti (5 MB)
        private const long MaxFileSize = 5 * 1024 * 1024;

        // 👉 Kabul edilen uzantılar
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        public CreateAboutWithFileValidator()
        {
            // Temel alan doğrulamalarını miras al
            Include(new CreateAboutValidator());

            // Ön görsel
            RuleFor(x => x.ImageFrontFile)
                .NotNull().WithMessage("You must select a front image.")
                .Must(IsValidImage)
                .WithMessage("Front image must be JPG, JPEG, or PNG and smaller than 5 MB.");

            // Arka görsel
            RuleFor(x => x.ImageBackFile)
                .NotNull().WithMessage("You must select a back image.")
                .Must(IsValidImage)
                .WithMessage("Back image must be JPG, JPEG, or PNG and smaller than 5 MB.");
        }
        private bool IsValidImage(IFormFile? file)
        {
            if (file is null) return false;

            // 1) MIME türü kontrolü
            if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                return false;

            // 2) Boyut kontrolü
            if (file.Length > MaxFileSize)
                return false;

            // 3) Uzantı kontrolü
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedExtensions.Contains(ext);
        }
    }
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutDTO>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.AboutID)
.GreaterThan(0).WithMessage("Invalid About ID.");

            Include(new CreateAboutValidator());
        }
    }
    public class UpdateAboutWithFileValidator : AbstractValidator<UpdateAboutWithFileDTO>
    {
        public UpdateAboutWithFileValidator()
        {
            RuleFor(x => x.AboutID)
            .GreaterThan(0).WithMessage("Invalid About ID.");

            Include(new CreateAboutWithFileValidator());
        }
    }
}
