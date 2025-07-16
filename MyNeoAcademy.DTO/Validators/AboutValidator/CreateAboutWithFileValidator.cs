using FluentValidation;
using Microsoft.AspNetCore.Http;
using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.DTO.Validators.InstructorValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.AboutValidator
{
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
}
