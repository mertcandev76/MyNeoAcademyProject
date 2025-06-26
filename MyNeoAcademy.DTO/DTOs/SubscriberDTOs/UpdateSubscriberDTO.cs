using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.SubscriberDTOs
{
    public class UpdateSubscriberDTO
    {
        public int SubscriberID { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
