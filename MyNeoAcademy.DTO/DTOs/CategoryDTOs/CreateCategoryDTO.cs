using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {


        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IconClass { get; set; }

    }
}
