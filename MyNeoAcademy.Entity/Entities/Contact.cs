using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string? MapUrl { get; set; }
        public string? Address { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
