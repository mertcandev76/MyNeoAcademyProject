﻿using System;
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

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
