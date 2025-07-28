using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Newsletter
    {
        public int NewsletterID { get; set; }            
        public string Email { get; set; } = null!; 
        public DateTime SubscribedDate { get; set; }
    }
}
