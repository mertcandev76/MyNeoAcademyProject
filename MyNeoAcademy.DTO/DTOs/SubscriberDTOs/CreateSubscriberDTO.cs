using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.SubscriberDTOs
{
    public class CreateSubscriberDTO
    {
        public string? Email { get; set; }

        public bool IsActive { get; set; } = false;  // Swagger’da default false olarak gösterir
    }
}
