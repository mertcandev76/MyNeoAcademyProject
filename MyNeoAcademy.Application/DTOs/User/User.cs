using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs.User
{
    public class ResultAppUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public List<string> Roles { get; set; } = new();
    }
    public class UpdateAppUserDTO : IHasId
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }

        public IFormFile? ProfileImageFile { get; set; }

    }
    public class AssignRolesDTO
    {
        public int UserId { get; set; }
        public List<string> Roles { get; set; } = new();
    }

}
