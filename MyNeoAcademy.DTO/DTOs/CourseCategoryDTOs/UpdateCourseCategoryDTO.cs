using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseCategoryDTOs
{
    public class UpdateCourseCategoryDTO : CreateCourseCategoryDTO
    {
        public int CourseCategoryID { get; set; }
    }
}
