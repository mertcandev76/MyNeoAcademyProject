using FluentValidation;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.Validators.InstructorValidator
{
    public class CreateInstructorValidator : AbstractValidator<CreateInstructorDTO>
    {
        public CreateInstructorValidator()
        {
            // FullName zorunludur, minimum 3 maksimum 100 karakter olmalıdır.
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            // Title isteğe bağlıdır, maksimum 50 karakterle sınırlandırılmıştır.
            RuleFor(x => x.Title)
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            // Bio isteğe bağlıdır, ancak maksimum 1000 karakter ile sınırlıdır.
            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Bio));

            // ImageUrl isteğe bağlıdır, ancak geçerli bir URL formatında olmalıdır.
            RuleFor(x => x.ImageUrl)
                .Must(BeAValidUrl).WithMessage("Image URL must be a valid URL.")
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

            // Facebook URL isteğe bağlıdır, URL formatı ve facebook alan adı kontrolü yapılır.
            RuleFor(x => x.FacebookUrl)
                .Must(BeAValidUrl).WithMessage("Facebook URL must be a valid URL.")
                .Must(url => url!.Contains("facebook.com")).WithMessage("Facebook URL must contain 'facebook.com'.")
                .When(x => !string.IsNullOrWhiteSpace(x.FacebookUrl));

            // Twitter URL isteğe bağlıdır, URL formatı ve twitter alan adı kontrolü yapılır.
            RuleFor(x => x.TwitterUrl)
                .Must(BeAValidUrl).WithMessage("Twitter URL must be a valid URL.")
                .Must(url => url!.Contains("twitter.com")).WithMessage("Twitter URL must contain 'twitter.com'.")
                .When(x => !string.IsNullOrWhiteSpace(x.TwitterUrl));

            // Website URL isteğe bağlıdır, URL formatı kontrolü yapılır.
            RuleFor(x => x.WebsiteUrl)
                .Must(BeAValidUrl).WithMessage("Website URL must be a valid URL.")
                .When(x => !string.IsNullOrWhiteSpace(x.WebsiteUrl));
        }


        // URL doğrulaması için yardımcı metot
        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
