﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CreateContactDTO
    {


        public string Name { get; set; } = null!;

        public string? Email { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }

    }
    public class ResultContactDTO : CreateContactDTO
    {
        public int ContactID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class UpdateContactDTO : CreateContactDTO
    {
        public int ContactID { get; set; }
    }
}
