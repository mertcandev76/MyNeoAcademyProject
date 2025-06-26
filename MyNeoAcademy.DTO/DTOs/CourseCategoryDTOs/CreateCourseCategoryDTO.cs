using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs
{
    public class CreateCourseCategoryDTO
    {

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool IsShown { get; set; }
    }
}
