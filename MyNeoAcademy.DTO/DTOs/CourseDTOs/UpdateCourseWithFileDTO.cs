using MyNeoAcademy.DTO.DTOs.SliderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CourseDTOs
{
    public class UpdateCourseWithFileDTO : CreateCourseWithFileDTO
    {
        public int CourseID { get; set; }
    }
}
