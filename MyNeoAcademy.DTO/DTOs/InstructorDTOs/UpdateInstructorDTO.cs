using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.InstructorDTOs
{
    public class UpdateInstructorDTO:CreateInstructorDTO
    {
        public int InstructorID { get; set; }
    }
}
