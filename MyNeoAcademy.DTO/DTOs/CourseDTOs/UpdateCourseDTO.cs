using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class UpdateCourseDTO:CreateCourseDTO
    {
        public int CourseID { get; set; }
    }
}
