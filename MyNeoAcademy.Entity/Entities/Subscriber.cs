using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Subscriber
    {
        public int SubscriberID { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = false;  // default false burada verilebilir
    }
}
