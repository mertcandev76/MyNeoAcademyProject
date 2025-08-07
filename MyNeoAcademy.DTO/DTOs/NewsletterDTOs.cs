using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs
{
    public class CreateNewsletterDTO
    {

        public string Email { get; set; } = null!;
    }
    public class ResultNewsletterDTO : CreateNewsletterDTO
    {
        public int NewsletterID { get; set; }
        public DateTime SubscribedDate { get; set; }
    }
    public class UpdateNewsletterDTO : CreateNewsletterDTO
    {
        public int NewsletterID { get; set; }
    }
}
