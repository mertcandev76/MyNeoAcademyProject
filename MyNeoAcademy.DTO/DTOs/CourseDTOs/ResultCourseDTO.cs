using MyNeoAcademy.DTO.DTOs.CategoryDTOs;
using MyNeoAcademy.DTO.DTOs.InstructorDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class ResultCourseDTO:CreateCourseDTO
    {
        public int CourseID { get; set; }

        public ResultCategoryDTO? Category { get; set; }

        public ResultInstructorDTO? Instructor { get; set; } // Navigasyon property
    }
}
