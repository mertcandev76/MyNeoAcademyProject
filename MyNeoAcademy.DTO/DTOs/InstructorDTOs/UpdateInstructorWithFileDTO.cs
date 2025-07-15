using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.InstructorDTOs
{
    public class UpdateInstructorWithFileDTO:CreateInstructorWithFileDTO
    {
        public int InstructorID { get; set; }
    }
}
