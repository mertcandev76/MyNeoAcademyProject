using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO:CreateCategoryDTO
    {
        public int CategoryID { get; set; }
    }
}
