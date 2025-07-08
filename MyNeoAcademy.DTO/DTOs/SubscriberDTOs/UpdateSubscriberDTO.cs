using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.SubscriberDTOs
{
    public class UpdateSubscriberDTO:CreateSubscriberDTO
    {
        public int SubscriberID { get; set; }
    }
}
