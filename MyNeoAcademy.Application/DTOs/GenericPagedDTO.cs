﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    // Pagination için generic PagedResultDTO
    public class PagedResultDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } 
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

}
