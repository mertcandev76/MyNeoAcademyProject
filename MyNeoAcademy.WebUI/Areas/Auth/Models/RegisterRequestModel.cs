using System.ComponentModel.DataAnnotations;

namespace MyNeoAcademy.WebUI.Areas.Auth.Models
{
    public class RegisterRequestModel
    {

        [Required(ErrorMessage = "Ad zorunludur.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Şifre tekrar zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = null!;

        [DataType(DataType.Upload)]
        public IFormFile? ProfileImageFile { get; set; }

    }
}
