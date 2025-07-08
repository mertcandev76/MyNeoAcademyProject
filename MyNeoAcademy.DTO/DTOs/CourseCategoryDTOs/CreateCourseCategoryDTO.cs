using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs
{
    public class CreateCourseCategoryDTO
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public bool IsShown { get; set; } = false;
    }
}
