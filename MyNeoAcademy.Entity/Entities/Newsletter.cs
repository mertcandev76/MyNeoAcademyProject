using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Newsletter
    {
        public int NewsletterID { get; set; }                // Primary Key
        public string Email { get; set; } = null!; // Kullanıcının girdiği email
        public DateTime SubscribedDate { get; set; }
    }
}
