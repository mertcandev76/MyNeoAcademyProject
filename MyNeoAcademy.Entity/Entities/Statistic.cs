using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class Statistic
    {
        public int StatisticID { get; set; }
        public string? SvgBase64 { get; set; }
        public string? ColorClass { get; set; }
        public int Count { get; set; }
        public string? Label { get; set; }  // Hem Description hem Unit yerine ortak kullanım
    }
}
