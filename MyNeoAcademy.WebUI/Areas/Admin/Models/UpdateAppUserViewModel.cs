using MyNeoAcademy.Application.DTOs.User;

namespace MyNeoAcademy.WebUI.Areas.Admin.Models
{
    public class UpdateAppUserViewModel
    {
        public UpdateAppUserDTO UserDto { get; set; } = new();

        public string? ProfileImageUrl { get; set; }  
    }

}
