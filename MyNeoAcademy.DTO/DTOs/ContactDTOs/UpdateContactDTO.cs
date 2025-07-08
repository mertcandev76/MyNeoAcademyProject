using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.ContactDTOs
{
    public class UpdateContactDTO:CreateContactDTO
    {
        public int ContactID { get; set; }
    }
}
