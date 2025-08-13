using MyNeoAcademy.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs.Role
{
    public class ResultRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class RoleBaseDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CreateRoleDTO : RoleBaseDTO
    {
    }

    public class UpdateRoleDTO : RoleBaseDTO, IHasId
    {
        public int Id { get; set; }
    }

}
