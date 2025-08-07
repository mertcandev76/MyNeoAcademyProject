using System.ComponentModel.DataAnnotations;

namespace MyNeoAcademy.WebUI.Areas.Auth.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
